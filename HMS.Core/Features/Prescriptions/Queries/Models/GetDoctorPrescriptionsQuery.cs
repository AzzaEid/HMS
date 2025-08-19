using HMS.Core.Bases;
using HMS.Core.Features.Prescriptions.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Prescriptions.Queries.Models
{
    public class GetDoctorPrescriptionsQuery : IRequest<Response<List<GetPrescriptionListResponse>>>
    {
        public int DoctorId { get; set; }
    }
}
