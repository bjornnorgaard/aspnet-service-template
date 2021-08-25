using System;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Ant.Platform.Exceptions
{
    public class PlatformException : Exception
    {
        public string ErrorMessage { get; }
        public PlatformError ErrorCode { get; }

        public PlatformException(PlatformError error) : base(error.Humanize(LetterCasing.Sentence))
        {
            ErrorCode = error;
            ErrorMessage = base.Message;
        }

        public BadRequestObjectResult ToBadRequestObjectResponse()
        {
            var response = new PlatformBadRequestResponse(ErrorCode, ErrorMessage);
            var result = new BadRequestObjectResult(response);
            return result;
        }
    }
}