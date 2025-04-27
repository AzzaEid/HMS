using FluentValidation;
using HMS.Core.Bases;
using HMS.Core.Behavior;
using Mapster;
using MediatR;
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

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient<ResponseHandler>();
            return services;
        }

    }
}
