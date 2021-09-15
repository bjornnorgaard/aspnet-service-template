using Microsoft.Extensions.Configuration;

namespace Ant.Platform.Options
{
    public class AuthenticationOptions : AbstractOptions
    {
        public string Authority { get; set; }
        public string Audience { get; set; }
        public string TestClientId { get; set; }
        public string TestClientSecret { get; set; }

        public AuthenticationOptions(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
