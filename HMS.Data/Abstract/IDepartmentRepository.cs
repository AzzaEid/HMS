using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentWithDoctorsAsync(int id);
    }
}
