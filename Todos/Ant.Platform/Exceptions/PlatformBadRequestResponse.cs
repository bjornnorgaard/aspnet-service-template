namespace Ant.Platform.Exceptions;

public class PlatformBadRequestResponse
{
    public int Code { get; init; }
    public string Message { get; init; } = "This should have been set by now.";
}