using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public static class CqrsServiceCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static void AddCQRS(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.ExportedTypes.Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in classTypes)
            {
                var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo()).ToArray();

                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)))
                {
                    services.AddScoped(handlerType.AsType(), type.AsType());
                }
                foreach (var handlerType in interfaces.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>)))
                {
                    services.AddScoped(handlerType.AsType(), type.AsType());
                }
            }
            
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        }
    }
}