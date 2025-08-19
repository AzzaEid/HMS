using FluentValidation;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Commends.Validators
{
    public class DeleteMedicationValidator : AbstractValidator<DeleteMedicationCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public DeleteMedicationValidator(
            IStringLocalizer<SharedResources> localizer,
            IMedicationService medicationService)
        {
            _localizer = localizer;
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
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.IDMustBeGreaterThanZero]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.Id)
                .MustAsync(async (medicationId, CancellationToken) => await MedicationExists(medicationId))
                .WithMessage(_localizer[SharedResourcesKeys.MedicationNotFound]);

            RuleFor(x => x.Id)
                .MustAsync(async (medicationId, CancellationToken) => await CanDeleteMedication(medicationId))
                .WithMessage(_localizer[SharedResourcesKeys.CannotDeleteMedicationWithPrescriptions]);
        }

        #region Helper Methods
        private async Task<bool> MedicationExists(int medicationId)
        {
            var medication = await _medicationService.GetMedicationByIdAsync(medicationId);
            return medication != null;
        }

        private async Task<bool> CanDeleteMedication(int medicationId)
        {
            var medication = await _medicationService.GetMedicationByIdAsync(medicationId);
            return medication?.Prescriptions == null || !medication.Prescriptions.Any();
        }
        #endregion
        #endregion
    }
}
