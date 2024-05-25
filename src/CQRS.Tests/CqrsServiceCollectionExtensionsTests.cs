using CQRS.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Tests;

public class CqrsServiceCollectionExtensionsTests
{
    [Fact]
    // ReSharper disable once InconsistentNaming
    public void AddCQRS()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCQRS(typeof(PingCommand).Assembly);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var commandDispatcher = serviceProvider.GetService<ICommandDispatcher>();
        Assert.NotNull(commandDispatcher);
        
        var queryDispatcher = serviceProvider.GetService<IQueryDispatcher>();
        Assert.NotNull(queryDispatcher);
        
        var pingQueryHandler = serviceProvider.GetService<IQueryHandler<PingQuery, string>>();
        Assert.NotNull(pingQueryHandler);
        
        var pingCommandHandler = serviceProvider.GetService<ICommandHandler<PingCommand, string>>();
        Assert.NotNull(pingCommandHandler);
        
        var internalPingCommandHandler = serviceProvider.GetService<ICommandHandler<InternalPingCommand, string>>();
        Assert.NotNull(internalPingCommandHandler);
    }
    
    [Fact]
    // ReSharper disable once InconsistentNaming
    public void AddCQRS_Scoped()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddCQRS(typeof(PingCommand).Assembly);
        var serviceProvider = serviceCollection.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();
        var scopedServiceProvider = scope.ServiceProvider;
        var commandDispatcher = scopedServiceProvider.GetService<ICommandDispatcher>();
        Assert.NotNull(commandDispatcher);
        
        var queryDispatcher = scopedServiceProvider.GetService<IQueryDispatcher>();
        Assert.NotNull(queryDispatcher);
            
        var pingQueryHandler = scopedServiceProvider.GetService<IQueryHandler<PingQuery, string>>();
        Assert.NotNull(pingQueryHandler);
        
        var pingCommandHandler = scopedServiceProvider.GetService<ICommandHandler<PingCommand, string>>();
        Assert.NotNull(pingCommandHandler);
        
        var internalPingCommandHandler = serviceProvider.GetService<ICommandHandler<InternalPingCommand, string>>();
        Assert.NotNull(internalPingCommandHandler);
    }
}