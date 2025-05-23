﻿using FluentValidation;
using HMS.Core.Resources;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResources> _localizer;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
                                  IStringLocalizer<SharedResources> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }
        /*
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            
        }
        */
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    var message = failures.Select(x => _localizer[$"{x.PropertyName}"] + ": " + _localizer[x.ErrorMessage]).FirstOrDefault();

                    throw new ValidationException(message, failures);

                }
            }
            return await next();
        }
    }

}
