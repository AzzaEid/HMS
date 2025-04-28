namespace HMS.Core.Features.Doctors.Queries.Results
{
    public class GetDoctorDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public SpecialtyDto Specialty { get; set; }
        public List<AppointmentShortDto> Appointments { get; set; }
        public List<PrescriptionShortDto> Prescriptions { get; set; }
    }
}
