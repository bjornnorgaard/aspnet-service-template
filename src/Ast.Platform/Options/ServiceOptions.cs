using Microsoft.Extensions.Configuration;

namespace Ast.Platform.Options;

public class ServiceOptions : AbstractOptions
{
    public string ConnectionString { get; set; }
    public bool HangfireEnabled { get; set; }
    public string ServiceName { get; set; }

    public ServiceOptions(IConfiguration configuration) : base(configuration)
    {
    }
}