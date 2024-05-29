using System.Diagnostics;

namespace Ast.Platform.Telemetry;

public static class ActivityCurrent
{
    public static void SetTag(string key, object value)
    {
        Activity.Current?.SetTag(key, value);
    }
}