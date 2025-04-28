using HMS.Core.Bases;
using HMS.Core.Features.Doctors.Queries.Results;
using MediatR;

namespace HMS.Core.Features.Departments.Queries.Models
{
    public class GetDepartmentDoctorsQuery : IRequest<Response<List<GetDoctorListDto>>>
    {
        public int Id { get; set; }
        public GetDepartmentDoctorsQuery(int id)
        {
            Id = id;
        }
    }
}
