using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ValidationSeparation.Commands;

namespace ValidationSeparation.Tests;

public class Tests
{
    [Fact]
    public async Task Validator_ShouldNotThrow_WhenValidCommand()
    {
        // Arrange
        var container = Program.BuildContainer();
        var mediator = container.GetRequiredService<IMediator>();
        var validCommand = new ExampleCommand
        {
            ExampleValue = "ValidValue"
        };

        // Act
        var action = async () => await mediator.Send(validCommand);

        // Assert
        await action.Should()
            .NotThrowAsync<ValidationException>();
    }
    
    [Fact]
    public async Task Validator_ShouldThrow_WhenInvalidCommand()
    {
        // Arrange
        var container = Program.BuildContainer();
        var mediator = container.GetRequiredService<IMediator>();
        var invalidCommand = new ExampleCommand
        {
            ExampleValue = "InvalidValue_1234567890"
        };

        // Act
        var action = async () => await mediator.Send(invalidCommand);

        // Assert
        await action.Should()
            .ThrowExactlyAsync<ValidationException>();
    }
}