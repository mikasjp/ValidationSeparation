using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ValidationSeparation.Commands;

public class ExampleCommand : IRequest
{
    public string ExampleValue { get; init; }
}

[UsedImplicitly]
public class ExampleCommandHandler : IRequestHandler<ExampleCommand>
{
    public Task Handle(ExampleCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Hello {request.ExampleValue}");
        return Task.CompletedTask;
    }
}