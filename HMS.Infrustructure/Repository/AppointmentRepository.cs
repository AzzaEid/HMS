using HMS.Data.Entities;
using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        #region Fields 
        private readonly DbSet<Appointment> _appointments;
        #endregion

        #region Constructor
        public AppointmentRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _appointments = dbcontext.Set<Appointment>();
        }
        #endregion

        public Task<IQueryable<Appointment>> GetAllAppointmentsQueryable()
        {
            return Task.FromResult(_appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .AsQueryable());
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>> GetPatientAppointmentsAsync(int patientId)
        {
            return await _appointments
                .Include(a => a.Doctor)
                .Where(a => a.PatientID == patientId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetDoctorAppointmentsAsync(int doctorId)
        {
            return await _appointments
                .Include(a => a.Patient)
                .Where(a => a.DoctorID == doctorId)
                .ToListAsync();
        }
    }
}
