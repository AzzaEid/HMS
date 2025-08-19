using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Medications.Commends.Models
{
    public class UpdateMedicationCommand : IRequest<Response<string>>
    {
        public int MedicationId { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
