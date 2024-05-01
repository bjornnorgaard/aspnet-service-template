using Xunit;

namespace Api.Todos.Tests.Unit;

[CollectionDefinition("UnitTests")]
public class UnitTestCollection : ICollectionFixture<UnitTestFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}