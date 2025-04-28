using HMS.Data.Entities;

namespace HMS.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllDepartmentsAsync();
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentWithDoctorsAsync(int id);
        Task<Department> CreateDepartmentAsync(Department department);
        Task UpdateDepartmentAsync(Department department);
        Task<bool> DeleteDepartmentAsync(int id);
    }
}
