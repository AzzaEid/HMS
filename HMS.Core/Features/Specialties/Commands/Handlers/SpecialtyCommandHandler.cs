using HMS.Core.Bases;
using HMS.Core.Features.Specialties.Commands.Models;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Specialties.Commands.Handlers
{
    public class SpecialtyCommandHandler : ResponseHandler,
        IRequestHandler<CreateSpecialtyCommand, Response<string>>,
        IRequestHandler<UpdateSpecialtyCommand, Response<string>>,
        IRequestHandler<DeleteSpecialtyCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly ISpecialtyService _specialtyService;
        #endregion

        #region Constructors
        public SpecialtyCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            ISpecialtyService specialtyService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _specialtyService = specialtyService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(CreateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            // التحقق من عدم وجود تخصص بنفس الاسم
            var nameExists = await _specialtyService.IsSpecialtyNameExistsAsync(request.SpecialtyName);
            if (nameExists)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.SpecialtyNameExists]);

            // إنشاء التخصص
            var specialty = _mapper.Map<Specialty>(request);
            var createdSpecialty = await _specialtyService.CreateSpecialtyAsync(specialty);

            return Success($"Specialty created successfully with ID: {createdSpecialty.SpecialtyId}");
        }

        public async Task<Response<string>> Handle(UpdateSpecialtyCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود التخصص
            var existingSpecialty = await _specialtyService.GetSpecialtyByIdAsync(request.SpecialtyId);
            if (existingSpecialty == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            // التحقق من عدم وجود تخصص آخر بنفس الاسم
            var nameExists = await _specialtyService.IsSpecialtyNameExistsAsync(request.SpecialtyName, request.SpecialtyId);
            if (nameExists)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.SpecialtyNameExists]);

            // تحديث التخصص
            var specialty = _mapper.Map<Specialty>(request);
            await _specialtyService.UpdateSpecialtyAsync(specialty);

            return Success("" + _sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteSpecialtyCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود التخصص
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(request.SpecialtyId);
            if (specialty == null)
                return NotFound<string>("" + _sharedResources[SharedResourcesKeys.NotFound]);

            // حذف التخصص
            var result = await _specialtyService.DeleteSpecialtyAsync(request.SpecialtyId);
            if (!result)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);

            return Success("" + _sharedResources[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
