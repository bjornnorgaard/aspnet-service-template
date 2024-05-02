using Hangfire;
using Newtonsoft.Json;

namespace Ast.Platform.Hangfire;

public static class HangfireConfigurationExtensions
{
    public static void AddMediatR(this IGlobalConfiguration configuration)
    {
        var jsonSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        configuration.UseSerializerSettings(jsonSettings);
    }
}