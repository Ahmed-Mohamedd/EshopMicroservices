using BuildingBlocks.CQRS;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildingBlocks.Behaviour
{
    public class ValidationBehaviour<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> Validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var ValidationResults =
                await Task.WhenAll(Validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var faliures = ValidationResults
                .Where(r=>r.Errors.Any())
                .SelectMany(r=> r.Errors)
                .ToList();

            if (faliures.Any()) 
                throw new ValidationException(faliures);

            return await next();
        }
    }
}
