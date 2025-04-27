namespace HMS.Core.Features.Patients.Queries.Results
{
    public class AppointmentDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string DoctorName { get; set; }
    }
}
