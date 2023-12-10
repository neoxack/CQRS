using BenchmarkDotNet.Attributes;
using CQRS.Benchmark.Utils;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Benchmark;

[MemoryDiagnoser]
public class ExecuteSimpleCommandBenchmark
{
    private IServiceProvider _serviceProvider = null!;
    private IServiceProvider _mediatrServiceProvider = null!;
    
    [GlobalSetup]
    public void Config()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCQRS(typeof(PingCommand).Assembly);
        _serviceProvider = serviceCollection.BuildServiceProvider();
        
        var mediatrServiceCollection = new ServiceCollection();
        mediatrServiceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PingRequest).Assembly));
        _mediatrServiceProvider = mediatrServiceCollection.BuildServiceProvider();
    }

    [Benchmark(Description = "CQRS", Baseline = true)]
    public void ExecuteCommand()
    {
        var commandDispatcher = _serviceProvider.GetService<ICommandDispatcher>()!;
        var _ = commandDispatcher.Execute(new PingCommand("ping"));
    }
    
    [Benchmark(Description = "MediatR")]
    public void ExecuteRequest()
    {
        var mediator = _mediatrServiceProvider.GetService<IMediator>()!;
        var _ = mediator.Send(new PingRequest("ping")).Result;
    }
}