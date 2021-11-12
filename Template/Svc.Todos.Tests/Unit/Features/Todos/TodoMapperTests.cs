using AutoMapper;
using Svc.Todos.Api;
using Xunit;

namespace Svc.Todos.Tests.Unit.Features.Todos
{
    public class TodoMapperTests
    {
        private readonly IMapper _uut;

        public TodoMapperTests()
        {
            _uut = new MapperConfiguration(c => c.AddMaps(typeof(Startup).Assembly)).CreateMapper();
        }

        [Fact]
        public void AssertValidConfiguration()
        {
            // Arrange

            // Act + Assert
            _uut.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}