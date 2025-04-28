using HMS.Data.Entities.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Data.Entities
{
    public class Doctor : User
    {
        [EmailAddress]
        public string Email { get; set; }

        public int? SpecialtyId { get; set; }
        public Specialty? Specialty { get; set; }

        // department the doctor working in  
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        // [InverseProperty(nameof(Department.Doctors))]
        public Department Department { get; set; }

        // department the doctor has managed
        // [InverseProperty(nameof(Department.ManagerDoctor))]
        public Department? ManagedDepartment { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Prescription> Prescriptions { get; set; } = new List<Prescription>();
    }
}
