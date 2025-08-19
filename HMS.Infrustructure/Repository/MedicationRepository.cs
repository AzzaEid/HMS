using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {
        #region Fields 
        private readonly DbSet<Medication> _medications;
        #endregion

        #region Constructor
        public MedicationRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _medications = dbcontext.Set<Medication>();
        }
        #endregion

        public async Task<IEnumerable<Medication>> GetAllMedicationsAsync()
        {
            return await _medications.ToListAsync();
        }

        public async Task<Medication> GetMedicationByIdAsync(int id)
        {
            return await _medications
                .Include(m => m.Prescriptions)
                .FirstOrDefaultAsync(m => m.MedicationId == id);
        }

        public async Task<List<Medication>> SearchMedicationsAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await _medications.ToListAsync();

            searchTerm = searchTerm.ToLower();
            return await _medications
                .Where(m => m.Name.ToLower().Contains(searchTerm))
                .ToListAsync();
        }

        public IQueryable<Medication> GetMedicationsQueryable()
        {
            return _medications.AsQueryable();
        }

        public async Task<List<Medication>> GetLowStockMedicationsAsync(int minQuantity = 10)
        {
            return await _medications
                .Where(m => m.Quantity <= minQuantity)
                .ToListAsync();
        }

        public async Task<bool> UpdateMedicationStockAsync(int medicationId, int newQuantity)
        {
            var medication = await _medications.FindAsync(medicationId);
            if (medication == null) return false;

            medication.Quantity = newQuantity;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsMedicationNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _medications.Where(m => m.Name.ToLower() == name.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(m => m.MedicationId != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
