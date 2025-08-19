using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        IQueryable<Doctor> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        IQueryable<Doctor> GetDoctorsOfDepartmentAsync(int id);
    }


}
