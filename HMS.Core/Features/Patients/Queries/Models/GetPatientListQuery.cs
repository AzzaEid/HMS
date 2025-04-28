using HMS.Core.Bases;
using HMS.Core.Features.Patients.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Patients.Queries.Models
{
    public class GetPatientListQuery : IRequest<Response<List<GetPatientListDto>>>
    {


    }
}
