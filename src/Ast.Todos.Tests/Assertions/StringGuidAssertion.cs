using FluentAssertions;

namespace Ast.Todos.Tests.Assertions;

internal static class StringGuidAssertion
{
    public static void MustBeValidGuid(this string s)
    {
        s.Should().NotBeNullOrWhiteSpace();
        var tryParseGuid = Guid.TryParse(s, out var parsedGuid);
        tryParseGuid.Should().BeTrue();
        parsedGuid.Should().NotBe(Guid.Empty);
    }
}