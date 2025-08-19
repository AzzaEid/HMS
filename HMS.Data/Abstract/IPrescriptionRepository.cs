using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface IPrescriptionRepository : IGenericRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> GetPrescriptionByIdAsync(int id);
        Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId);
        Task<IEnumerable<Prescription>> GetDoctorPrescriptionsAsync(int doctorId);
        IQueryable<Prescription> GetPrescriptionsQueryable();
        Task<List<Prescription>> GetPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
