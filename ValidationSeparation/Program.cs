using MediatR;
using FluentValidation;
using System.Threading.Tasks;
using ValidationSeparation.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace ValidationSeparation
{
    internal class Program
    {
        private static async Task Main(string[] _)
        {
            var container = BuildContainer();

            var mediator = container.GetRequiredService<IMediator>();
            
            var command = new ExampleCommand
            {
                ExampleValue = "Test"   // valid input
                // ExampleValue = "qwertyuiopasdfghjklzxcvbnm" // invalid input
            };
            
            await mediator.Send(command);
        }

        internal static ServiceProvider BuildContainer()
        {
            var container = new ServiceCollection()
                .AddMediatR(c =>
                {
                    c.RegisterServicesFromAssemblyContaining<Program>();
                })
                .Scan(scan => scan
                    .FromAssemblyOf<Program>()
                    .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime())
                .BuildServiceProvider();
            return container;
        }
    }
}
