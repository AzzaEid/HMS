using MediatR;
using HMS.Data;
using HMS.Data.Entities;
using HMS.Core.Features.Patients.Queries.Results;
using HMS.Core.Bases;

namespace HMS.Core.Features.Patients.Queries.Models
{
    public class GetPatientListQuery : IRequest<Response<List<GetPatientListDto>>>
    {


    }
}
