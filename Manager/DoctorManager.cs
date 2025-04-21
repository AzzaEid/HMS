using Hospital_Management_System.Data;
using Hospital_Management_System.Entities;

namespace Hospital_Management_System.Manager
{
    public class DoctorManager
    {
        private readonly HMSDBContext _dbContext;

        public DoctorManager(HMSDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddDoctor(Doctor doctor)
        {
            _dbContext.Doctors.Add(doctor);
            Console.WriteLine($"Doctor {doctor.Name} Added successfully");
        }
        public void RemoveDoctor(int id)
        {
            var doctor = _dbContext.Doctors.Find(id);
            if (doctor != null)
            {
                _dbContext.Doctors.Remove(doctor);
                Console.WriteLine($"Doctor {doctor.Name} deleted successfully");
            }
            Console.WriteLine("There's no Doctor with this ID");
        }
        public Doctor? GetDoctor(int id)
        {
            return _dbContext.Doctors.FirstOrDefault(d => d.Id == id);
        }
        public List<Doctor> GetDoctors()
        {
            return _dbContext.Doctors.ToList();
        }
        public void UpdateDoctor(Doctor doctor)
        {
            if (doctor != null)
            {
                _dbContext.Doctors.Update(doctor);
                Console.WriteLine("Updated Successfully");
            }

        }
    }
}
