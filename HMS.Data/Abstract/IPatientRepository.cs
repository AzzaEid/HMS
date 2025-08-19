using HMS.Data.Bases;
using HMS.Data.Entities;

namespace HMS.Data.Abstract
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        public Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}
