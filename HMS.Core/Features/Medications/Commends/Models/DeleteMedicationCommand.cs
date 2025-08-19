using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Medications.Commends.Models
{
    public class DeleteMedicationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
