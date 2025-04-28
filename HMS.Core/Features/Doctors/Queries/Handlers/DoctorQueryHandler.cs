using HMS.Core.Bases;
using HMS.Core.Features.Doctors.Queries.Models;
using HMS.Core.Features.Doctors.Queries.Results;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Doctors.Queries.Handlers
{
    public class DoctorQueryHandler : ResponseHandler, IRequestHandler<GetDoctorListQuery, Response<List<GetDoctorListDto>>>
                                                     , IRequestHandler<GetDoctorByIdQuery, Response<GetDoctorDetailDto>>
    {
        private readonly IDoctorService _doctorService;

        public DoctorQueryHandler(IDoctorService doctorService,
                                IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _doctorService = doctorService;
        }

        public Task<Response<List<GetDoctorListDto>>> Handle(GetDoctorListQuery request, CancellationToken cancellationToken)
        {
            var doctors = _doctorService.GetAllDoctorsQueryable();
            var doctorsListDto = doctors.Adapt<List<GetDoctorListDto>>();
            return Task.FromResult(Success(doctorsListDto));
        }

        public async Task<Response<GetDoctorDetailDto>> Handle(GetDoctorByIdQuery request, CancellationToken cancellationToken)
        {

            var doctor = await _doctorService.GetDoctorByIdAsync(request.Id);

            if (doctor == null)
                return NotFound<GetDoctorDetailDto>($"Doctor with ID {request.Id} not found.");

            var doctorDto = doctor.Adapt<GetDoctorDetailDto>();
            return Success(doctorDto);
        }
    }
}

