using MediatR;

namespace BuildingBlocks.Core.CQRS;

//https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/in-generic-modifier
//https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/out-generic-modifier
//TODO : out keyword is here for market TResponse is only used for return in handler implementation
//TODO: IQuery interface for separation of concerns when in this interface we can implementation specification configuration
public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
{
}


/// Represents a void type, since <see cref="System.Void"/> is not a valid return type in C#.
public interface IQuery : IRequest
{ }