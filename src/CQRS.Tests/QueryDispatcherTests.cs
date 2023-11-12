using CQRS.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Tests;

public class QueryDispatcherTests
{
    [Fact]
    public void ShouldThrowWithoutHandler()
    {
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var commandDispatcher = new QueryDispatcher(serviceProvider);

        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = commandDispatcher.Ask(new PingQuery("ping"));
        });
    }
    
    [Fact]
    public void ExecuteCommand()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<IQueryHandler<PingQuery, string>, PingQueryHandler>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var queryDispatcher = new QueryDispatcher(serviceProvider);
        
        var result = queryDispatcher.Ask(new PingQuery("ping"));
        
        Assert.Equal("ping", result);
    }
    
    
}