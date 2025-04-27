using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Patients.Commands.Modles
{
    public class DeletePatientCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
