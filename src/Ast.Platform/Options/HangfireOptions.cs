using Microsoft.Extensions.Configuration;

namespace Ast.Platform.Options;

public class HangfireOptions : AbstractOptions
{
    public string ConnectionString { get; set; }
    public bool EnabledHangfire { get; set; }

    public HangfireOptions(IConfiguration configuration) : base(configuration)
    {
    }
}