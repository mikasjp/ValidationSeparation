using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ValidationSeparation.Commands;

public class ExampleCommand : IRequest
{
    public string ExampleValue { get; set; }
}

public class ExampleCommandHandler : IRequestHandler<ExampleCommand>
{
    public Task Handle(ExampleCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello {request.ExampleValue}");
        return Task.CompletedTask;
    }
}