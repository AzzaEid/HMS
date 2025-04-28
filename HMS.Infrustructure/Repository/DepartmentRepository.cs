using HMS.Data.Entities;
using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;

        public DepartmentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departments
                .Include(d => d.ManagerDoctor)
                .ToListAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            return await _departments
                .Include(d => d.ManagerDoctor)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task<Department> GetDepartmentWithDoctorsAsync(int id)
        {
            return await _departments
                .Include(d => d.ManagerDoctor)
                .Include(d => d.Doctors)
                .FirstOrDefaultAsync(d => d.DepartmentId == id);
        }
    }

}
