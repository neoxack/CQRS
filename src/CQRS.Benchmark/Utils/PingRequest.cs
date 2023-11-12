using MediatR;

namespace CQRS.Benchmark.Utils;

public class PingRequest : IRequest<string>
{
    public string Value { get; }

    public PingRequest(string value)
    {
        Value = value;
    }
}

public class PingHandler : IRequestHandler<PingRequest, string>
{
    public Task<string> Handle(PingRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(request.Value);
    }
}