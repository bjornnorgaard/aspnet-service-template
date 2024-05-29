using System.Diagnostics;

namespace Ast.Platform.Telemetry;

internal static class TelemetrySource
{
    internal const string ProjectName = "Platform";
    internal static readonly ActivitySource Source = new(ProjectName);
}