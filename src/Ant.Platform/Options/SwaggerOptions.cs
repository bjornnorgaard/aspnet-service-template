using Microsoft.Extensions.Configuration;

namespace Ant.Platform.Options;

public class SwaggerOptions : AbstractOptions
{
    public string ApplicationTitle { get; set; }

    public SwaggerOptions(IConfiguration configuration) : base(configuration)
    {
    }
}