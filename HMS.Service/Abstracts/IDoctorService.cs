using HMS.Data.Entities;

namespace HMS.Service.Abstracts
{
    public interface IDoctorService
    {
        IQueryable<Doctor> GetAll();
        IQueryable<Doctor> GetAllDoctorsQueryable();
        Task<Doctor> GetDoctorByIdAsync(int id);
        Task<Doctor> CreateDoctorAsync(Doctor doctor);
        Task UpdateDoctorAsync(Doctor doctor);
        Task DeleteDoctorAsync(int id);
        IQueryable<Doctor> GetDoctorsOfDepartmentAsync(int id);
    }
}
