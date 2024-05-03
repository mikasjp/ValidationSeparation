using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using ValidationSeparation.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ValidationSeparation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = new ServiceCollection()
                .AddMediatR(typeof(Program).Assembly)
                .Scan(scan => scan
                    .FromAssemblyOf<Program>()
                        .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime()
                        .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                            .AsImplementedInterfaces()
                            .WithScopedLifetime())
                .BuildServiceProvider();

            var mediator = container.GetRequiredService<IMediator>();
            
            var command = new ExampleCommand
            {
                ExampleValue = "Test"   // valid input
                // ExampleValue = "qwertyuiopasdfghjklzxcvbnm" // invalid input
            };
            
            await mediator.Send(command);
        }
    }
}
