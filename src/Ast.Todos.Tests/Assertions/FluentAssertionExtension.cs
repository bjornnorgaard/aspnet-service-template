using FluentAssertions.Execution;

namespace Ast.Todos.Tests.Assertions;

public static class FluentAssertionExtension
{
    public static void ShouldBeSuccess(this HttpResponseMessage actualValue)
    {
        if (actualValue.IsSuccessStatusCode) return;
        var msg = actualValue.Content.ReadAsStringAsync().Result;
        throw new AssertionFailedException(msg);
    }
}
