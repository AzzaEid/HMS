using HMS.Core.Bases;
using HMS.Core.Features.Doctors.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Doctors.Queries.Models
{
    public class GetDoctorByIdQuery : IRequest<Response<GetDoctorDetailDto>>
    {
        public int Id { get; set; }
    }
}
