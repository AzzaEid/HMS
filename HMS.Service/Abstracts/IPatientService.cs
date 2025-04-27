using HMS.Data.Entities;
using HMS.Data.Helper;

namespace HMS.Service.Abstracts
{
    public interface IPatientService
    {
        public Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task UpdatePatientAsync(Patient patient);
        Task DeletePatientAsync(int id);
        public IQueryable<Patient> GetAllPatientsQueryable();
        public IQueryable<Patient> FilterPatientPaginatedQuerable(PatientOrderingEnum order, string? search);
    }
}
