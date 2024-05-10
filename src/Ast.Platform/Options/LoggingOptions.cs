using Microsoft.Extensions.Configuration;

namespace Ast.Platform.Options;

public class LoggingOptions : AbstractOptions
{
    public string ApplicationName { get; set; }

    public LoggingOptions(IConfiguration configuration) : base(configuration)
    {
    }
}