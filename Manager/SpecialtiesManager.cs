using Hospital_Management_System.Data;
using Hospital_Management_System.Entities;

namespace Hospital_Management_System.Manager
{
    public class SpecialtiesManager
    {
        private readonly HMSDBContext _dbContext;

        public SpecialtiesManager(HMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Specialty> GetSpecialties()
        {
            return _dbContext.Specialties.ToList();
        }
        public void AddSpecialty(Specialty specialty)
        {
            if (specialty != null)
            {
                _dbContext.Specialties.Add(specialty);
                Console.WriteLine("Specialty Added ");
            }
        }

    }
}
