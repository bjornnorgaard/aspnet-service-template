using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Ast.Todos.Tests.Assertions;

public static class FluentAssertionExtension
{
    public static async Task<HttpResponseMessageAssertions> ShouldBeSuccess(this HttpResponseMessage actualValue)
    {
        if (actualValue.IsSuccessStatusCode)
        {
            return new HttpResponseMessageAssertions(actualValue);
        }

        var msg = await actualValue.Content.ReadAsStringAsync();
        throw new AssertionFailedException(msg);
    }
}