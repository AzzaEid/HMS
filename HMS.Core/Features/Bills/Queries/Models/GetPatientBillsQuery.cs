using HMS.Core.Bases;
using HMS.Core.Features.Bills.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Bills.Queries.Models
{
    public class GetPatientBillsQuery : IRequest<Response<List<GetBillListResponse>>>
    {
        public int PatientId { get; set; }
    }
}
