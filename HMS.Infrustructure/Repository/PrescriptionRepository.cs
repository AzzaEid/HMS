using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
    {
        #region Fields 
        private readonly DbSet<Prescription> _prescriptions;
        #endregion

        #region Constructor
        public PrescriptionRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _prescriptions = dbcontext.Set<Prescription>();
        }
        #endregion

        public async Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync()
        {
            return await _prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Include(p => p.Bills)
                .ToListAsync();
        }

        public async Task<Prescription> GetPrescriptionByIdAsync(int id)
        {
            return await _prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Include(p => p.Bills)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId)
        {
            return await _prescriptions
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Include(p => p.Bills)
                .Where(p => p.PatientID == patientId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Prescription>> GetDoctorPrescriptionsAsync(int doctorId)
        {
            return await _prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Medications)
                .Include(p => p.Bills)
                .Where(p => p.DoctorID == doctorId)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }

        public IQueryable<Prescription> GetPrescriptionsQueryable()
        {
            return _prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Include(p => p.Bills)
                .AsQueryable();
        }

        public async Task<List<Prescription>> GetPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _prescriptions
                .Include(p => p.Patient)
                .Include(p => p.Doctor)
                .Include(p => p.Medications)
                .Where(p => p.Date >= startDate && p.Date <= endDate)
                .OrderByDescending(p => p.Date)
                .ToListAsync();
        }
    }
}
