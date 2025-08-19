using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface ISpecialtyRepository : IGenericRepository<Specialty>
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();
        Task<Specialty> GetSpecialtyByIdAsync(int id);
        Task<bool> IsSpecialtyNameExistsAsync(string name, int? excludeId = null);
    }
}
