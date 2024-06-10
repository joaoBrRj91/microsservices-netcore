namespace BuildingBlocks.Core.Exceptions;

public class InternalServerErrorException : Exception
{
    public string? Detail { get; }

    public InternalServerErrorException(string message, Exception exception) : base(message, exception) { }

    public InternalServerErrorException(string message, string detail, Exception exception) : base(message, exception)
        => Detail = detail;

}
