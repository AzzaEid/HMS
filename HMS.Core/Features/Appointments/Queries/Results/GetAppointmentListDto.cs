namespace HMS.Core.Features.Appointments.Queries.Results
{
    public class GetAppointmentListDto
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
