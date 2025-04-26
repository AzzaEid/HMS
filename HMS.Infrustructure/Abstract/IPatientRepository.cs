using HMS.Data.Entities;
using HMS.Infrustructure.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Infrustructure.Abstract
{
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        public Task<IEnumerable<Patient>> GetAllPatientsAsync();
    }
}
