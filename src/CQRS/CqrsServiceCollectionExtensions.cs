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
                var interfaces = type.ImplementedInterfaces.Where(e=>e.IsGenericType).Select(i => i.GetTypeInfo()).ToArray();
                if (interfaces.Length > 0)
                {
                    foreach (var handlerType in interfaces)
                    {
                        var typeDefinition = handlerType.GetGenericTypeDefinition();
                        if (typeDefinition == typeof(IQueryHandler<,>))
                        {
                            services.AddScoped(handlerType.AsType(), type.AsType());
                        }
                        else
                        if (typeDefinition == typeof(ICommandHandler<,>))
                        {
                            services.AddScoped(handlerType.AsType(), type.AsType());
                        }
                    }
                }
            }
            
            services.AddSingleton<ICommandDispatcher, CommandDispatcher>(sp => new CommandDispatcher(sp));
            services.AddSingleton<IQueryDispatcher, QueryDispatcher>(sp => new QueryDispatcher(sp));
        }
    }
}