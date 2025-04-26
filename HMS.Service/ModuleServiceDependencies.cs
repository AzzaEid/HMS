using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Repository;
using HMS.Service.Abstracts;
using HMS.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IPatientService, PatientService>();
            return services;
        }

    }
}
