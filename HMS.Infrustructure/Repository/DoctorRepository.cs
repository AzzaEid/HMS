using HMS.Data.Entities;
using HMS.Infrustructure.Abstract;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        #region Fields 
        private readonly DbSet<Doctor> _doctors;
        #endregion

        #region Constructor
        public DoctorRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _doctors = dbcontext.Set<Doctor>();
        }
        #endregion

        public IQueryable<Doctor> GetAllDoctorsAsync()
        {
            return _doctors
                .Include(d => d.Specialty)
                .Include(d => d.Department)
                .AsQueryable();
        }

        public async Task<Doctor> GetDoctorByIdAsync(int id)
        {
            return await _doctors
                .Include(d => d.Specialty)
                .Include(d => d.Appointments)
                .Include(d => d.Prescriptions)
                .Include(d => d.Department)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public IQueryable<Doctor> GetDoctorsOfDepartmentAsync(int id)
        {
            return _doctors
               .Where(d => d.DepartmentId == id)
               .AsQueryable();
        }
    }
}
