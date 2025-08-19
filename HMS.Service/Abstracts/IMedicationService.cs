using HMS.Data.Entities;
using HMS.Data.Helper.Enums;

namespace HMS.Service.Abstracts
{
    public interface IMedicationService
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(int id);
        Task<Medication> CreateMedicationAsync(Medication medication);
        Task<Medication> UpdateMedicationAsync(Medication medication);
        Task<bool> DeleteMedicationAsync(int id);
        Task<List<Medication>> SearchMedicationsAsync(string searchTerm);
        Task<bool> IsMedicationNameExistsAsync(string name, int? excludeId = null);
        Task<List<Medication>> GetLowStockMedicationsAsync(int threshold = 10);
        IQueryable<Medication> FilterMedicationsPaginatedQueryable(MedicationOrderingEnum orderBy, string search = null);
        Task<bool> UpdateMedicationStockAsync(int medicationId, int newQuantity);
    }
}
