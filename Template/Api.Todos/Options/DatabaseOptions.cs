using Ant.Platform.Options;

namespace Api.Todos.Options
{
    public class DatabaseOptions : AbstractOptions
    {
        public string TodoDatabase { get; set; }

        public DatabaseOptions(IConfiguration configuration) : base(configuration)
        {
        }
    }
}