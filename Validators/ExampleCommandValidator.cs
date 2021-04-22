using FluentValidation;
using ValidationSeparation.Commands;

namespace ValidationSeparation.Validators
{
    public class ExampleCommandValidator : AbstractValidator<ExampleCommand>
    {
        public static int MaximumExampleValueLength => 20;

        public ExampleCommandValidator()
        {
            RuleFor(x => x.ExampleValue).NotEmpty();
            RuleFor(x => x.ExampleValue).MaximumLength(MaximumExampleValueLength);
        }
    }
}