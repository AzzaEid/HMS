using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Repository;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HMS.Core
{
    public static class ModuleCoreDependencies
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {
            services.AddMediatR(cnf => cnf.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            services.AddMapster();
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
