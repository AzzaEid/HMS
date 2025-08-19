using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        Task<IEnumerable<Medication>> GetAllMedicationsAsync();
        Task<Medication> GetMedicationByIdAsync(int id);
        Task<List<Medication>> SearchMedicationsAsync(string searchTerm);
        IQueryable<Medication> GetMedicationsQueryable();
        Task<List<Medication>> GetLowStockMedicationsAsync(int minQuantity = 10);
        Task<bool> UpdateMedicationStockAsync(int medicationId, int newQuantity);
        Task<bool> IsMedicationNameExistsAsync(string name, int? excludeId = null);

    }
}
