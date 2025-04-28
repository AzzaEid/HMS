using System.ComponentModel.DataAnnotations.Schema;

namespace HMS.Data.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [ForeignKey(nameof(ManagerDoctor))]
        public int? ManagerDoctorId { get; set; }

        //   [InverseProperty(nameof(Doctor.ManagedDepartment))]
        public Doctor? ManagerDoctor { get; set; } = null;

        //[InverseProperty(nameof(Doctor.Department))]
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
