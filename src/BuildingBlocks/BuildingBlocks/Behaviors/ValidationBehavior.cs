using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System.Collections;

namespace BuildingBlocks.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
         where TRequest : ICommand<TRequest>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
           var context = new ValidationContext<TRequest>(request);
            var failures = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            var failureList = failures.SelectMany(result => result.Errors).Where(f => f != null).ToList();
            if (failureList.Count != 0)
            {
                throw new ValidationException(failureList);
            }
            return await next();
        }
    }
}
