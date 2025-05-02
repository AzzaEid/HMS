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
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();




            return services;
        }

    }
}
