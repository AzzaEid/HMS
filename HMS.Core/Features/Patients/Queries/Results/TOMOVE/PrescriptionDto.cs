namespace HMS.Core.Features.Patients.Queries.Results
{
    public class PrescriptionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public List<string> Medications { get; set; }
    }
}
