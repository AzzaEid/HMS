namespace HMS.Core.Features.Doctors.Queries.Results
{
    public class AppointmentShortDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string PatientName { get; set; }
    }
}
