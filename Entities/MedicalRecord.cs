namespace Hospital_Management_System.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; } 

        // Navigation Property
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
}
