using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        
        private static readonly ConcurrentDictionary<Type, Type> QueryHandlerTypes = new();

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Ask<TResult>(IQuery<TResult> query)
        {
            var queryType = query.GetType();
            var handlerType = QueryHandlerTypes.GetOrAdd(queryType, static qType =>
            {
                var handlerType = typeof(IQueryHandler<,>).MakeGenericType(qType, typeof(TResult));
                return handlerType;
            });
            dynamic handler = _serviceProvider.GetRequiredService(handlerType);
            return (TResult)handler.Ask((dynamic)query);
        }
    }
}