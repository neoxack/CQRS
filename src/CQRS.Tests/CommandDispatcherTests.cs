using CQRS.Tests.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Tests;

public class CommandDispatcherTests
{
    [Fact]
    public void ShouldThrowWithoutHandler()
    {
        var serviceCollection = new ServiceCollection();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var commandDispatcher = new CommandDispatcher(serviceProvider);

        Assert.Throws<InvalidOperationException>(() =>
        {
            var _ = commandDispatcher.Execute(new PingCommand("ping"));
        });
    }
    
    [Fact]
    public void ExecuteCommand()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICommandHandler<PingCommand, string>, PingCommandHandler>();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        
        var commandDispatcher = new CommandDispatcher(serviceProvider);
        
        var result = commandDispatcher.Execute(new PingCommand("ping"));
        
        Assert.Equal("ping", result);
    }
    
    
}