using MediatR;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ValidationSeparation.Behaviors;

[UsedImplicitly]
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }

    public Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        return next();
    }
}