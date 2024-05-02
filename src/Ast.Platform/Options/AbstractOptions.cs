using Microsoft.Extensions.Configuration;

namespace Ast.Platform.Options;

public abstract class AbstractOptions
{
    protected AbstractOptions(IConfiguration configuration)
    {
        var thisTypeName = GetType().Name;
        configuration.GetSection(thisTypeName).Bind(this);
    }
}