using System.Diagnostics.Metrics;

namespace Ast.Platform.Telemetry;

internal static class TelemetryMeters
{
    internal static readonly Meter Meter = new(TelemetryConfig.ProjectName);
}