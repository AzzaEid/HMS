using HMS.Data.Entities;
using HMS.Infrustructure.Abstract;
using HMS.Service.Abstracts;

namespace HMS.Service.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllDepartmentsAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public async Task<Department> GetDepartmentWithDoctorsAsync(int id)
        {
            return await _departmentRepository.GetDepartmentWithDoctorsAsync(id);
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            return await _departmentRepository.AddAsync(department);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task<bool> DeleteDepartmentAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department != null)
            {
                await _departmentRepository.DeleteAsync(department);
                return true;
            }
            return false;
        }
    }
}
