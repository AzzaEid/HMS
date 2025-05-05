using HMS.Data.Entities;
using HMS.Data.Helper.Enums;

namespace HMS.Service.Abstracts
{
    public interface IAppointmentService
    {
        IQueryable<Appointment> GetAllAppointmentsQueryable();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
        Task<List<Appointment>> GetPatientAppointmentsAsync(int patientId);
        Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId);
        IQueryable<Appointment> FilterAppointmentPaginatedQuerable(AppointmentOrderingEnum orderBy, string search = null);
    }
}
