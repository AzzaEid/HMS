using HMS.Core.Bases;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Commends.Handlers
{
    public class MedicationCommandHandler : ResponseHandler,
        IRequestHandler<CreateMedicationCommand, Response<string>>,
        IRequestHandler<UpdateMedicationCommand, Response<string>>,
        IRequestHandler<DeleteMedicationCommand, Response<string>>,
        IRequestHandler<UpdateMedicationStockCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public MedicationCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IMedicationService medicationService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _medicationService = medicationService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            // التحقق من عدم وجود دواء بنفس الاسم
            var nameExists = await _medicationService.IsMedicationNameExistsAsync(request.Name);
            if (nameExists)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.MedicationNameExists]);

            // إنشاء الدواء
            var medication = _mapper.Map<Medication>(request);
            var createdMedication = await _medicationService.CreateMedicationAsync(medication);

            return Success($"Medication created successfully with ID: {createdMedication.MedicationId}");
        }

        public async Task<Response<string>> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الدواء
            var existingMedication = await _medicationService.GetMedicationByIdAsync(request.MedicationId);
            if (existingMedication == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.MedicationNotFound]);

            // التحقق من عدم وجود دواء آخر بنفس الاسم
            var nameExists = await _medicationService.IsMedicationNameExistsAsync(request.Name, request.MedicationId);
            if (nameExists)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.MedicationNameExists]);

            // تحديث الدواء
            var medication = _mapper.Map<Medication>(request);
            await _medicationService.UpdateMedicationAsync(medication);

            return Success("" + _sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الدواء
            var medication = await _medicationService.GetMedicationByIdAsync(request.Id);
            if (medication == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.MedicationNotFound]);

            // حذف الدواء
            var result = await _medicationService.DeleteMedicationAsync(request.Id);
            if (!result)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.CannotDeleteMedicationWithPrescriptions]);

            return Success("" + _sharedResources[SharedResourcesKeys.Deleted]);
        }

        public async Task<Response<string>> Handle(UpdateMedicationStockCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الدواء
            var medication = await _medicationService.GetMedicationByIdAsync(request.MedicationId);
            if (medication == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.MedicationNotFound]);

            // تحديث المخزون
            var result = await _medicationService.UpdateMedicationStockAsync(request.MedicationId, request.NewQuantity);
            if (!result)
                return BadRequest<string>("" + _sharedResources[SharedResourcesKeys.UpdateFailed]);

            return Success("" + _sharedResources[SharedResourcesKeys.Updated]);
        }
        #endregion
    }
}
