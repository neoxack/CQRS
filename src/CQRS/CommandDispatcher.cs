using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        
        private static readonly ConcurrentDictionary<Type, Type> CommandHandlerTypes = new();
        
        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Execute<TResult>(ICommand<TResult> command)
        {
            var commandType = command.GetType();
            var handlerType = CommandHandlerTypes.GetOrAdd(commandType, static cmdType =>
            {
                var handlerType = typeof(ICommandHandler<,>).MakeGenericType(cmdType, typeof(TResult));
                return handlerType;
            });
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return (TResult)handler.Handle((dynamic)command);
        }
    }
}