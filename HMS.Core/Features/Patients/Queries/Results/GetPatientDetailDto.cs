namespace HMS.Core.Features.Patients.Queries.Results
{
    public class GetPatientDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public List<AppointmentDto> Appointments { get; set; }
        public List<PrescriptionDto> Prescriptions { get; set; }
    }
}
