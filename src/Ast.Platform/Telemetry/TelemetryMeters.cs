using System.Diagnostics.Metrics;

namespace Ast.Platform.Telemetry;

internal static class TelemetryMeters
{
    internal static readonly Meter Meter = new(TelemetrySource.ProjectName);
    internal static readonly Counter<int> FeatureInvokationCount = Meter.CreateCounter<int>("feature_invocation_count");
}
