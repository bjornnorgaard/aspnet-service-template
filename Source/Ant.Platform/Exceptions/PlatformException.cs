using System;
using Humanizer;
using Microsoft.AspNetCore.Mvc;

namespace Ant.Platform.Exceptions
{
    public class PlatformException : Exception
    {
        public string ErrorMessage { get; set; }
        public PlatformError ErrorCode { get; set; }
        
        public PlatformException(PlatformError error) : base(error.Humanize(LetterCasing.Sentence))
        {
            ErrorCode = error;
            ErrorMessage = base.Message;
        }

        public BadRequestObjectResult ToResponseObject()
        {
            var result = new BadRequestObjectResult(new
            {
                ErrorCode,
                ErrorMessage
            });
            
            return result;
        } 
    }
}