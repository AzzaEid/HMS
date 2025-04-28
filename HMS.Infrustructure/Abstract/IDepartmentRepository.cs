using HMS.Data.Entities;
using HMS.Infrustructure.Bases;

namespace HMS.Infrustructure.Abstract
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentWithDoctorsAsync(int id);
    }
}
