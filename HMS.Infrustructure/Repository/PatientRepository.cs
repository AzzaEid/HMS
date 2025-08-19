using HMS.Data.Abstract;
using HMS.Data.Entities;
using HMS.Infrustructure.Bases;
using HMS.Infrustructure.Data;
using Microsoft.EntityFrameworkCore;

namespace HMS.Infrustructure.Repository
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        #region Fields 
        private readonly DbSet<Patient> _patients;

        #endregion

        #region Constructor
        public PatientRepository(ApplicationDBContext dbcontext) : base(dbcontext)
        {
            _patients = dbcontext.Set<Patient>();
        }

        #endregion

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patients.ToListAsync();
        }
    }
}
