using Microsoft.Extensions.Configuration;

namespace AST.Platform.Options;

public class LoggingOptions : AbstractOptions
{
    public string ApplicationName { get; set; }
    public string ElasticSearchUrl { get; set; }

    public LoggingOptions(IConfiguration configuration) : base(configuration)
    {
    }
}