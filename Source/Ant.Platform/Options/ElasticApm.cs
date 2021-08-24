using Microsoft.Extensions.Configuration;

namespace Ant.Platform.Options
{
    public class ElasticApm : AbstractOptions
    {
        public string ServiceName { get; set; }
        public string ServerUrls { get; set; }
        
        public ElasticApm(IConfiguration configuration) : base(configuration)
        {
        }
    }
}