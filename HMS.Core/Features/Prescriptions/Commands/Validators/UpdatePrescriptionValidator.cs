using FluentValidation;
using HMS.Core.Features.Prescriptions.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Prescriptions.Commands.Validators
{
    public class UpdatePrescriptionValidator : AbstractValidator<UpdatePrescriptionCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public UpdatePrescriptionValidator(
            IStringLocalizer<SharedResources> localizer,
            IPrescriptionService prescriptionService,
            IPatientService patientService,
            IDoctorService doctorService,
            IMedicationService medicationService)
        {
            _localizer = localizer;
            _prescriptionService = prescriptionService;
            _patientService = patientService;
            _doctorService = doctorService;
            _medicationService = medicationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.InvalidId]);

            RuleFor(x => x.PatientID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.InvalidPatientId]);

            RuleFor(x => x.DoctorID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.InvalidDoctorId]);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .LessThanOrEqualTo(DateTime.Now.AddDays(1)).WithMessage(_localizer[SharedResourcesKeys.InvalidDate]);

            RuleFor(x => x.MedicationIds)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.MedicationRequired])
                .Must(list => list.Count > 0).WithMessage(_localizer[SharedResourcesKeys.MedicationRequired]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (id, CancellationToken) =>
                {
                    var prescription = await _prescriptionService.GetPrescriptionByIdAsync(id);
                    return prescription != null;
                }).WithMessage(_localizer[SharedResourcesKeys.PrescriptionNotFound]);

            RuleFor(x => x.PatientID)
                .MustAsync(async (patientId, CancellationToken) =>
                {
                    var patient = await _patientService.GetPatientByIdAsync(patientId);
                    return patient != null;
                }).WithMessage(_localizer[SharedResourcesKeys.PatientNotFound]);

            RuleFor(x => x.DoctorID)
                .MustAsync(async (doctorId, CancellationToken) =>
                {
                    var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
                    return doctor != null;
                }).WithMessage(_localizer[SharedResourcesKeys.DoctorNotFound]);

            RuleFor(x => x.MedicationIds)
                .MustAsync(async (medicationIds, CancellationToken) =>
                {
                    if (medicationIds == null || !medicationIds.Any()) return false;

                    foreach (var medicationId in medicationIds)
                    {
                        var medication = await _medicationService.GetMedicationByIdAsync(medicationId);
                        if (medication == null) return false;
                    }
                    return true;
                }).WithMessage(_localizer[SharedResourcesKeys.InvalidMedicationIds]);
        }
        #endregion
    }
}
