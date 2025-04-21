using Hospital_Management_System.Entities;

namespace Hospital_Management_System.DTO
{
    public class MedicalRecordDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }

    }
}
