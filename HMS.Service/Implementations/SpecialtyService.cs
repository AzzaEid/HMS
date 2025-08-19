using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Service.Abstracts;

namespace HMS.Service.Implementations
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public SpecialtyService(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        public async Task<IEnumerable<Specialty>> GetAllSpecialtiesAsync()
        {
            return await _specialtyRepository.GetAllSpecialtiesAsync();
        }

        public async Task<Specialty> GetSpecialtyByIdAsync(int id)
        {
            return await _specialtyRepository.GetSpecialtyByIdAsync(id);
        }

        public async Task<Specialty> CreateSpecialtyAsync(Specialty specialty)
        {
            return await _specialtyRepository.AddAsync(specialty);
        }

        public async Task<Specialty> UpdateSpecialtyAsync(Specialty specialty)
        {
            await _specialtyRepository.UpdateAsync(specialty);
            return await GetSpecialtyByIdAsync(specialty.SpecialtyId);
        }

        public async Task<bool> DeleteSpecialtyAsync(int id)
        {
            var specialty = await _specialtyRepository.GetByIdAsync(id);
            if (specialty != null)
            {
                await _specialtyRepository.DeleteAsync(specialty);
                return true;
            }
            return false;
        }

        public async Task<bool> IsSpecialtyNameExistsAsync(string name, int? excludeId = null)
        {
            return await _specialtyRepository.IsSpecialtyNameExistsAsync(name, excludeId);
        }
    }
}
