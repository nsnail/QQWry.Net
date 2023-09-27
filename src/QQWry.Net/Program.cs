using System.Diagnostics;
using QQWry.Net;

Banner.Show();

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(x => x.ListenAnyIP(61022));
var app    = builder.Build();
var logger = app.Services.GetRequiredService<ILogger<Program>>();

logger.LogInformation("正在加载数据...");
var sw = new Stopwatch();
sw.Start();
var ipUtils = new IpUtils();
sw.Stop();
logger.LogInformation("已加载 {DataCount} 条 in {UsedTime}ms", ipUtils.DataCount, sw.ElapsedMilliseconds);

app.MapGet("/", async context => {
    var ipStr = context.Request.Query["ip"].FirstOrDefault();
    if (ipStr == null) {
        await context.Response.WriteAsJsonAsync(ErrorResult());
        return;
    }

    uint ip;
    try {
        ip = IpUtils.IpV4ToUInt32(ipStr);
    }
    catch (Exception) {
        await context.Response.WriteAsJsonAsync(ErrorResult(ipStr));
        return;
    }

    var region = ipUtils.Query(ip);
    await context.Response.WriteAsJsonAsync(new Result(region == null ? -1 : 0, ipStr, region));
});
app.Run();
return;

static Result ErrorResult(string ipStr = null)
{
    return new Result(-1, ipStr, null);
}