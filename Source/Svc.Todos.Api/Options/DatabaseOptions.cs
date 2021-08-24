using Microsoft.Extensions.Configuration;
using Svc.Platform.Options;

namespace Svc.Todos.Api.Options
{
    public class DatabaseOptions : AbstractOptions
    {
        public string TodoDatabase { get; set; }

        public DatabaseOptions(IConfiguration configuration) : base(configuration)
        {
        }
    }
}