using System.Diagnostics;

namespace Ast.Platform.Telemetry;

internal static class TelemetryConfig
{
    internal const string ProjectName = "Platform";
    internal static readonly ActivitySource Source = new(ProjectName);
}