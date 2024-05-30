using MediatR;

namespace BuildingBlocks.Core.CQRS;

//https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/in-generic-modifier
//https://learn.microsoft.com/pt-br/dotnet/csharp/language-reference/keywords/out-generic-modifier
//TODO : out keyword is here for market TResponse is only used for return in handler implementation
//TODO: ICommand interface for separation of concerns when in this interface we can implementation for exemple validation of data command
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}


/// Represents a void type, since <see cref="System.Void"/> is not a valid return type in C#.
public interface ICommand : IRequest
{ }