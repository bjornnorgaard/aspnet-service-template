using Microsoft.Extensions.Configuration;

namespace Ant.Platform.Options;

public class OpenTelemetryOptions : AbstractOptions
{
    public string ApplicationName { get; set; }
    public string OpenTelemetryEndpoint { get; set; }
    
    public OpenTelemetryOptions(IConfiguration configuration) : base(configuration)
    {
    }
}