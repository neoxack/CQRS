using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS
{
    public static class CqrsServiceCollectionExtensions
    {
        // ReSharper disable once InconsistentNaming
        public static void AddCQRS(this IServiceCollection services, Assembly assembly)
        {
            var classTypes = assembly.DefinedTypes;
            foreach (var type in classTypes)
            {
                if (!type.IsClass || type.IsAbstract)
                {
                    continue;
                }
                var implementedInterfaces = type.GetInterfaces();
                foreach (var implementedInterface in implementedInterfaces)
                {
                    if (!implementedInterface.IsGenericType)
                    {
                        continue;
                    }
                    var typeDefinition = implementedInterface.GetGenericTypeDefinition();
                    if (typeDefinition == typeof(IQueryHandler<,>) || typeDefinition == typeof(ICommandHandler<,>))
                    {
                        services.AddScoped(implementedInterface, type);
                    }
                }
            }
            
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        }
    }
}