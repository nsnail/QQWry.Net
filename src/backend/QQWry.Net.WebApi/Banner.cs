using System.Reflection;
using System.Text.Json;
using Spectre.Console;

namespace QQWry.Net;

internal static class Banner
{
    public static void Show()
    {
        AnsiConsole.WriteLine();
        var gridInfo = new Grid().AddColumn(new GridColumn().NoWrap().PadRight(10).Width(50))
                                 .AddColumn(new GridColumn().NoWrap())
                                 .Expand();
        foreach (var kv in GetEnvironmentInfo()) {
            _ = gridInfo.AddRow(kv.Key, kv.Value.ToString()!.EscapeMarkup());
        }

        var gridWrap      = new Grid().AddColumn();
        var entryAssembly = Assembly.GetEntryAssembly();
        var assemblyName  = entryAssembly!.GetName();
        _ = gridWrap.AddRow(new FigletText(assemblyName.Name!).Color(Color.Green));
        _ = gridWrap.AddRow(gridInfo);
        AnsiConsole.Write(new Panel(gridWrap)
                          .Header(Assembly.GetExecutingAssembly()
                                          .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
                                          .InformationalVersion)
                          .Expand());
        AnsiConsole.WriteLine();
    }

    private static Dictionary<string, object> GetEnvironmentInfo()
    {
        var ret = typeof(Environment).GetProperties(BindingFlags.Public | BindingFlags.Static)
                                     .Where(x => x.Name is not (nameof(Environment.StackTrace)
                                                                or nameof(Environment.NewLine)))
                                     .ToDictionary(x => x.Name, x => x.GetValue(null));
        _ = ret.TryAdd("Environment", JsonSerializer.Serialize(Environment.GetEnvironmentVariables()));

        return ret;
    }
}