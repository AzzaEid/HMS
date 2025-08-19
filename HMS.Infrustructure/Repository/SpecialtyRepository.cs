using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class SpecialtyRepository : GenericRepository<Specialty>, ISpecialtyRepository
    {
        #region Fields 
        private readonly DbSet<Specialty> _specialties;
        #endregion

        #region Constructor
        public SpecialtyRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _specialties = dbcontext.Set<Specialty>();
        }
        #endregion

        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync()
        {
            return await _specialties.ToListAsync();
        }

        public async Task<Specialty> GetSpecialtyByIdAsync(int id)
        {
            return await _specialties.FirstOrDefaultAsync(s => s.SpecialtyId == id);
        }

        public async Task<bool> IsSpecialtyNameExistsAsync(string name, int? excludeId = null)
        {
            var query = _specialties.Where(s => s.SpecialtyName.ToLower() == name.ToLower());
            if (excludeId.HasValue)
            {
                query = query.Where(s => s.SpecialtyId != excludeId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
