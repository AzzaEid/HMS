using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Commands.Models
{
    public class CreatePrescriptionCommand : IRequest<Response<string>>
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public List<int> MedicationIds { get; set; } = new List<int>();
    }
}
