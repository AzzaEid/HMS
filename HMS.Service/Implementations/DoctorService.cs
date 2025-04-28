using HMS.Data.Entities;
using HMS.Infrustructure.Abstract;
using HMS.Service.Abstracts;

namespace HMS.Service.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }
        public IQueryable<Doctor> GetAll()
        {
            return _doctorRepository.GetTableNoTracking();
        }
        public IQueryable<Doctor> GetAllDoctorsQueryable()
        {
            return _doctorRepository.GetAllDoctorsAsync();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctorRepository.GetDoctorByIdAsync(id);
        }
        #region change later with identity 
        public async Task<Doctor> CreateDoctorAsync(Doctor doctor)
        {
            return await _doctorRepository.AddAsync(doctor);
        }

        public async Task UpdateDoctorAsync(Doctor doctor)
        {
            await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task DeleteDoctorAsync(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);
            if (doctor != null)
            {
                await _doctorRepository.DeleteAsync(doctor);
            }
        }
        #endregion
        public IQueryable<Doctor> GetDoctorsOfDepartmentAsync(int id)
        {
            return _doctorRepository.GetDoctorsOfDepartmentAsync(id);
        }
    }

}
