using HMS.Core.Bases;
using HMS.Data.Entities.Enums;
using MediatR;

namespace HMS.Core.Features.Doctors.Commands.Modles
{
    public class CreateDoctorCommand : IRequest<Response<int>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public int? SpecialtyId { get; set; }
        public int DepartmentId { get; set; }

    }
}
