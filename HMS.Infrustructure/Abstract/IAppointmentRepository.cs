using HMS.Data.Entities;
using HMS.Infrustructure.Bases;

namespace HMS.Infrustructure.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAllAppointmentsQueryable();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<List<Appointment>> GetPatientAppointmentsAsync(int patientId);
        Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId);
    }
}
