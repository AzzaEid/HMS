namespace HMS.Core.Features.Appointments.Queries.Results
{
    public class GetAppointmentDetailDto
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public int DoctorID { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpecialty { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
