using HMS.Core.Bases;
using MediatR;

namespace HMS.Core.Features.Doctors.Commands.Modles
{
    public class DeleteDoctorCommand : IRequest<Response<bool>>
    {
        public int Id { get; set; }
    }
}
