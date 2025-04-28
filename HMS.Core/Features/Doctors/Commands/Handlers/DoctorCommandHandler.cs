using HMS.Core.Bases;
using HMS.Core.Features.Doctors.Commands.Modles;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Doctors.Commands.Handlers
{

    public class DoctorCommandHandler : ResponseHandler, IRequestHandler<CreateDoctorCommand, Response<int>>
                                                       , IRequestHandler<UpdateDoctorCommand, Response<bool>>
                                                       , IRequestHandler<DeleteDoctorCommand, Response<bool>>
    {
        private readonly IDoctorService _doctorService;

        public DoctorCommandHandler(IDoctorService doctorService,
            IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _doctorService = doctorService;
        }

        public async Task<Response<int>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = new Doctor
            {
                NameAr = request.NameAr,
                NameEn = request.NameEn,
                Age = request.Age,
                Gender = request.Gender,
                ContactNumber = request.ContactNumber,
                Email = request.Email,
                SpecialtyId = request.SpecialtyId >= 0 || request.SpecialtyId == null ? request.SpecialtyId : null,
                DepartmentId = request.DepartmentId,
            };

            var result = await _doctorService.CreateDoctorAsync(doctor);
            return Success(result.Id);
        }

        public async Task<Response<bool>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
                return NotFound<bool>($"Doctor with ID {request.Id} not found.");

            doctor.NameAr = request.NameAr;
            doctor.NameEn = request.NameEn;

            doctor.Age = request.Age;
            doctor.Gender = request.Gender;
            doctor.ContactNumber = request.ContactNumber;
            doctor.Email = request.Email;
            doctor.SpecialtyId = request.SpecialtyId;

            await _doctorService.UpdateDoctorAsync(doctor);
            return Success(true);
        }



        public async Task<Response<bool>> Handle(DeleteDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
                return NotFound<bool>($"Doctor with ID {request.Id} not found.");

            await _doctorService.DeleteDoctorAsync(request.Id);
            return Success(true);
        }
    }
}


