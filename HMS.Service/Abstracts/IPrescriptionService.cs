using HMS.Data.Entities;
using HMS.Data.Helper.Enums;

namespace HMS.Service.Abstracts
{
    public interface IPrescriptionService
    {
        Task<IEnumerable<Prescription>> GetAllPrescriptionsAsync();
        Task<Prescription> GetPrescriptionByIdAsync(int id);
        Task<Prescription> CreatePrescriptionAsync(Prescription prescription, List<int> medicationIds);
        Task<Prescription> UpdatePrescriptionAsync(Prescription prescription, List<int> medicationIds);
        Task<bool> DeletePrescriptionAsync(int id);
        Task<IEnumerable<Prescription>> GetPatientPrescriptionsAsync(int patientId);
        Task<IEnumerable<Prescription>> GetDoctorPrescriptionsAsync(int doctorId);
        IQueryable<Prescription> FilterPrescriptionsPaginatedQueryable(PrescriptionOrderingEnum orderBy, string search = null);
        Task<List<Prescription>> GetPrescriptionsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
