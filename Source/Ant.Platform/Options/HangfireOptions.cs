using Microsoft.Extensions.Configuration;

namespace Svc.Platform.Options
{
    public class HangfireOptions : AbstractOptions
    {
        public string ConnectionString { get; set; }
        
        public HangfireOptions(IConfiguration configuration) : base(configuration)
        {
        }
    }
}