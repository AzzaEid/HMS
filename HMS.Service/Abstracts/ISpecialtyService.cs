using HMS.Data.Entities;

namespace HMS.Service.Abstracts
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync();
        Task<Specialty> GetSpecialtyByIdAsync(int id);
        Task<Specialty> CreateSpecialtyAsync(Specialty specialty);
        Task<Specialty> UpdateSpecialtyAsync(Specialty specialty);
        Task<bool> DeleteSpecialtyAsync(int id);
        Task<bool> IsSpecialtyNameExistsAsync(string name, int? excludeId = null);
    }
}
