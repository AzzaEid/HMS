using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Medications.Commends.Models
{
    public class UpdateMedicationStockCommand : IRequest<Response<string>>
    {
        public int MedicationId { get; set; }
        public int NewQuantity { get; set; }
    }
}
