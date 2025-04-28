using HMS.Core.Bases;
using HMS.Core.Features.Doctors.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Doctors.Queries.Models
{

    public class GetDoctorListQuery : IRequest<Response<List<GetDoctorListDto>>>
    {
    }
}
