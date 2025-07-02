using FluentValidation;
using MediatR;
using PocMediatR.Common.Exceptions;

namespace PocMediatR.Application.Features
{
    public abstract class HandlerBase<TRequest, TResponse>(IEnumerable<AbstractValidator<TRequest>> validations)
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (validations != null)
            {
                var list = new List<InvalidParameterValueException>();

                foreach (var validator in validations)
                {
                    var validationResult = await validator.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);
                    foreach (var error in validationResult.Errors)
                    {
                        var exception = new InvalidParameterValueException(error.PropertyName);
                        list.Add(exception);
                    }
                }

                if (list.Count > 0)
                    throw new AggregateException(list);
            }

            return await ProcessHandler(request, cancellationToken);
        }

        public abstract Task<TResponse> ProcessHandler(TRequest request, CancellationToken cancellationToken);  
    }
}
