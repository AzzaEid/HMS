using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Infrustructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IDoctorRepository, DoctorRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();




            return services;
        }
    }
}
