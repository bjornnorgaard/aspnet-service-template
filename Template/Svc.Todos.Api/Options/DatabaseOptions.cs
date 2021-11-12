using Ant.Platform.Options;
using Microsoft.Extensions.Configuration;

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