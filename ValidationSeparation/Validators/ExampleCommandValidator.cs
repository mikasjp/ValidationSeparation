using FluentValidation;
using JetBrains.Annotations;
using ValidationSeparation.Commands;

namespace ValidationSeparation.Validators;

[UsedImplicitly]
public class ExampleCommandValidator : AbstractValidator<ExampleCommand>
{
    private static int MaximumExampleValueLength => 20;

    public ExampleCommandValidator()
    {
        RuleFor(x => x.ExampleValue).NotEmpty();
        RuleFor(x => x.ExampleValue).MaximumLength(MaximumExampleValueLength);
    }
}