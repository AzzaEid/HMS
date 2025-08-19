using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Data.Helper.Enums;
using HMS.Service.Abstracts;

namespace HMS.Service.Implementations
{
    public class MedicationService : IMedicationService
    {
        private readonly IMedicationRepository _medicationRepository;

        public MedicationService(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
        {
            return await _medicationRepository.GetAllMedicationsAsync();
        }

        public async Task<Medication> GetMedicationByIdAsync(int id)
        {
            return await _medicationRepository.GetMedicationByIdAsync(id);
        }

        public async Task<Medication> CreateMedicationAsync(Medication medication)
        {
            return await _medicationRepository.AddAsync(medication);
        }

        public async Task<Medication> UpdateMedicationAsync(Medication medication)
        {
            await _medicationRepository.UpdateAsync(medication);
            return await GetMedicationByIdAsync(medication.MedicationId);
        }

        public async Task<bool> DeleteMedicationAsync(int id)
        {
            var medication = await _medicationRepository.GetMedicationByIdAsync(id);
            if (medication != null)
            {
                // التحقق من عدم وجود وصفات مرتبطة
                if (medication.Prescriptions != null && medication.Prescriptions.Any())
                {
                    return false; // لا يمكن حذف الدواء إذا كان مرتبط بوصفات
                }

                await _medicationRepository.DeleteAsync(medication);
                return true;
            }
            return false;
        }

        public async Task<List<Medication>> SearchMedicationsAsync(string searchTerm)
        {
            return await _medicationRepository.SearchMedicationsAsync(searchTerm);
        }

        public async Task<bool> IsMedicationNameExistsAsync(string name, int? excludeId = null)
        {
            return await _medicationRepository.IsMedicationNameExistsAsync(name, excludeId);
        }

        public async Task<List<Medication>> GetLowStockMedicationsAsync(int threshold = 10)
        {
            return await _medicationRepository.GetLowStockMedicationsAsync(threshold);
        }

        public IQueryable<Medication> FilterMedicationsPaginatedQueryable(MedicationOrderingEnum orderBy, string search = null)
        {
            var query = _medicationRepository.GetMedicationsQueryable();

            // Apply search if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.ToLower();
                query = query.Where(m => m.Name.ToLower().Contains(search));
            }

            // Apply ordering
            query = orderBy switch
            {
                MedicationOrderingEnum.NameAsc => query.OrderBy(m => m.Name),
                MedicationOrderingEnum.NameDesc => query.OrderByDescending(m => m.Name),
                MedicationOrderingEnum.PriceAsc => query.OrderBy(m => m.Price),
                MedicationOrderingEnum.PriceDesc => query.OrderByDescending(m => m.Price),
                MedicationOrderingEnum.QuantityAsc => query.OrderBy(m => m.Quantity),
                MedicationOrderingEnum.QuantityDesc => query.OrderByDescending(m => m.Quantity),
                _ => query.OrderBy(m => m.Name)
            };

            return query;
        }

        public async Task<bool> UpdateMedicationStockAsync(int medicationId, int newQuantity)
        {
            var medication = await _medicationRepository.GetByIdAsync(medicationId);
            if (medication != null)
            {
                medication.Quantity = newQuantity;
                await _medicationRepository.UpdateAsync(medication);
                return true;
            }
            return false;
        }
    }
}