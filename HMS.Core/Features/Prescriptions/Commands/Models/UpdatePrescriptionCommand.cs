using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Commands.Models
{
    public class UpdatePrescriptionCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; }
        public List<int> MedicationIds { get; set; } = new List<int>();
    }
}
