using Microsoft.Extensions.Configuration;

namespace Ast.Platform.Options;

public class SwaggerOptions : AbstractOptions
{
    public string ApplicationTitle { get; set; }

    public SwaggerOptions(IConfiguration configuration) : base(configuration)
    {
    }
}