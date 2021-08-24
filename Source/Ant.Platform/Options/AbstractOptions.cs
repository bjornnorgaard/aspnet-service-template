using Microsoft.Extensions.Configuration;

namespace Svc.Platform.Options
{
    public abstract class AbstractOptions
    {
        protected AbstractOptions(IConfiguration configuration)
        {
            var thisTypeName = GetType().Name;
            configuration.GetSection(thisTypeName).Bind(this);
        }
    }
}