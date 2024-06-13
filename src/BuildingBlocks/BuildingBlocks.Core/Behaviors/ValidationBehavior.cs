using BuildingBlocks.Core.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Core.Behaviors;

//List of validators because one command may have more one validator
public sealed class ValidationBehavior<TRequest, TResponse>
    (IEnumerable<IValidator<TRequest>> validators,
    ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        //Validation Business logic of command
        var requestName = nameof(request);
        logger.LogInformation("[ValidationBehavior-{requestName}] : {request}", request, requestName);


        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators
            .Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(v => v.Errors.Count != 0)
            .SelectMany(v => v.Errors)
            .ToList();

        if (failures.Count > 0)
            throw new ValidationException(failures);

        return await next();
    }
}
