using Hospital_Management_System.Data;
using Hospital_Management_System.Entities;

namespace Hospital_Management_System.Manager
{
    public class PatientManager
    {
        private readonly HMSDBContext _dbContext;

        public PatientManager(HMSDBContext dbContext) {
            _dbContext = dbContext;
        }
        public void AddPatient(Patient patient)
        {
            _dbContext.Patients.Add(patient);
            Console.WriteLine($"Patient {patient.Name} Added successfully");
        }
        public void RemovePatient(int id) {
            var patient = _dbContext.Patients.Find(id);
            if (patient != null)
            {
                _dbContext.Patients.Remove(patient);
                Console.WriteLine($"Patient {patient.Name} deleted successfully");
            }
            Console.WriteLine("There's no patient with this ID");
        }
        public Patient? GetPatient(int id) {
            return _dbContext.Patients.FirstOrDefault(p => p.Id==id);
        }
        public List<Patient> GetPatients()
        {
            return _dbContext.Patients.ToList();
        }
        public void UpdatePatient(Patient patient)
        {
            if (patient != null)
            {
                _dbContext.Patients.Update(patient);
                Console.WriteLine("Updated Successfully");
            }
            
        }

    }
}
