#pragma warning disable CA1848

using Microsoft.Extensions.Primitives;
using QQWry.Net;

Banner.Show();

var builder = WebApplication.CreateBuilder(args);
_ = builder.Services.AddCors();
builder.WebHost.ConfigureKestrel(x => x.ListenAnyIP(61022));
var app = builder.Build();
_ = app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("已加载 {DataCount} 条IP数据", IpUtils.DataCount);

app.MapGet("/", async context => {
    var ipStr = context.Request.Query["ip"];
    if (ipStr == StringValues.Empty || ipStr.Count == 0) {
        await context.Response.WriteAsJsonAsync(ErrorResult());
        return;
    }

    IEnumerable<uint> ip;
    try {
        ip = ipStr.Select(IpUtils.IpV4ToUInt32);
    }
    catch (Exception) {
        await context.Response.WriteAsJsonAsync(ErrorResult(ipStr));
        return;
    }

    var region = IpUtils.Query(ip);
    await context.Response.WriteAsJsonAsync(region.Select(
                                                x => new Result(x.Region == null ? -1 : 0, IpUtils.UInt32ToIpV4(x.Ip)
                                                              , x.Region)));
});
app.Run();

static Result[] ErrorResult(string ipStr = null)
{
    return [new Result(-1, ipStr, null)];
}