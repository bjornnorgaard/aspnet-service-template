namespace Ant.Platform.Exceptions
{
    public class PlatformBadRequestResponse
    {
        public PlatformError Code { get; }
        public string Message { get; }

        public PlatformBadRequestResponse(PlatformError code, string message)
        {
            Code = code;
            Message = message;
        }
    }
}