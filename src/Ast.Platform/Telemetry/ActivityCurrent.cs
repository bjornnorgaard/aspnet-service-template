using System.Diagnostics;

namespace Ast.Platform.Telemetry;

public static class ActivityCurrent
{
    public static void SetTag(string key, object value)
    {
        Activity.Current?.SetTag(key, value);
    }

    public static void AddEvent(string name)
    {
        Activity.Current?.AddEvent(new ActivityEvent(name));
    }
}
