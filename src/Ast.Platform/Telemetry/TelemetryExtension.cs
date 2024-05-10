using System.Diagnostics;

namespace Ast.Platform.Telemetry;

internal static class TelemetryExtension
{
    private const string FeatureNameTag = "feature.name";
    private const string FeatureValidationTag = "feature.validation";


    internal static Activity StartFeatureActivity(this ActivitySource source, string name)
    {
        var activity = source.StartActivity(name, ActivityKind.Server);
        activity?.AddTag(FeatureNameTag, name);
        return activity;
    }

    internal static void AddValidationDisabled(this Activity activity)
    {
        activity?.AddTag(FeatureValidationTag, "disabled");
    }

    internal static void AddValidationResult(this Activity activity, bool passed)
    {
        if (!passed) activity?.SetStatus(ActivityStatusCode.Error, "Validation failed");

        activity?.AddTag(FeatureValidationTag, passed ? "passed" : "failed");
    }
}