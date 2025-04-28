using HMS.Data.Entities;
using HMS.Infrustructure.Bases;

namespace HMS.Infrustructure.Abstract
{
    public interface IDoctorRepository : IGenericRepository<Doctor>
    {
        IQueryable<Doctor> GetAllDoctorsAsync();
        Task<Doctor> GetDoctorByIdAsync(int id);
        IQueryable<Doctor> GetDoctorsOfDepartmentAsync(int id);
    }


}
