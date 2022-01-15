using Microsoft.Extensions.Configuration;

namespace Ant.Platform.Options;

public class HangfireOptions : AbstractOptions
{
    public string ConnectionString { get; set; }

    public HangfireOptions(IConfiguration configuration) : base(configuration)
    {
    }
}