using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Commands.Models
{
    public class DeletePrescriptionCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
