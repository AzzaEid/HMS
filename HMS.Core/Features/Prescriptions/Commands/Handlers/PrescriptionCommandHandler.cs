using HMS.Core.Bases;
using HMS.Core.Features.Prescriptions.Commands.Models;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Prescriptions.Commands.Handlers
{
    public class PrescriptionCommandHandler : ResponseHandler,
        IRequestHandler<CreatePrescriptionCommand, Response<string>>,
        IRequestHandler<UpdatePrescriptionCommand, Response<string>>,
        IRequestHandler<DeletePrescriptionCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        #endregion

        #region Constructors
        public PrescriptionCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IPrescriptionService prescriptionService,
            IPatientService patientService,
            IDoctorService doctorService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _prescriptionService = prescriptionService;
            _patientService = patientService;
            _doctorService = doctorService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PatientNotFound]);

            // التحقق من وجود الطبيب
            var doctor = await _doctorService.GetDoctorByIdAsync(request.DoctorID);
            if (doctor == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.DoctorNotFound]);

            // إنشاء الوصفة
            var prescription = _mapper.Map<Prescription>(request);
            var createdPrescription = await _prescriptionService.CreatePrescriptionAsync(prescription, request.MedicationIds);

            return Success($"Prescription created successfully with ID: {createdPrescription.Id}");
        }

        public async Task<Response<string>> Handle(UpdatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الوصفة
            var existingPrescription = await _prescriptionService.GetPrescriptionByIdAsync(request.Id);
            if (existingPrescription == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PrescriptionNotFound]);

            // التحقق من وجود المريض
            var patient = await _patientService.GetPatientByIdAsync(request.PatientID);
            if (patient == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PatientNotFound]);

            // التحقق من وجود الطبيب
            var doctor = await _doctorService.GetDoctorByIdAsync(request.DoctorID);
            if (doctor == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.DoctorNotFound]);

            // تحديث الوصفة
            var prescription = _mapper.Map<Prescription>(request);
            await _prescriptionService.UpdatePrescriptionAsync(prescription, request.MedicationIds);

            return Success("" + _sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(DeletePrescriptionCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الوصفة
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(request.Id);
            if (prescription == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PrescriptionNotFound]);

            // حذف الوصفة
            var result = await _prescriptionService.DeletePrescriptionAsync(request.Id);
            if (!result)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);

            return Success(_sharedResources[SharedResourcesKeys.Deleted] + "");
        }
        #endregion
    }
}
