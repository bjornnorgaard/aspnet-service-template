using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Ast.Platform.Exceptions;

public class PlatformException : Exception
{
    private string Error { get; }
    private PlatformError Code { get; }

    public PlatformException(PlatformError error) : base(error.Humanize(LetterCasing.Sentence))
    {
        Code = error;
        Error = base.Message;
    }

    public BadRequestObjectResult ToBadRequestObjectResponse()
    {
        var response = new PlatformBadRequestResponse { Code = (int)Code, Message = Error };
        var result = new BadRequestObjectResult(response);
        return result;
    }
}