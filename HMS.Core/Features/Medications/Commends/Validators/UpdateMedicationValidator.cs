using FluentValidation;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Commends.Validators
{
    public class UpdateMedicationValidator : AbstractValidator<UpdateMedicationCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public UpdateMedicationValidator(
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
            RuleFor(x => x.MedicationId)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.IDMustBeGreaterThanZero]);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(200).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis200]);

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.QuantityMustBeGreaterThanZero]);

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.PriceMustBeGreaterThanZero]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.MedicationId)
                .MustAsync(async (medicationId, CancellationToken) => await MedicationExists(medicationId))
                .WithMessage(_localizer[SharedResourcesKeys.MedicationNotFound]);

            RuleFor(x => x)
                .MustAsync(async (command, CancellationToken) =>
                    !await _medicationService.IsMedicationNameExistsAsync(command.Name, command.MedicationId))
                .WithMessage(_localizer[SharedResourcesKeys.MedicationNameExists]);
        }

        #region Helper Methods
        private async Task<bool> MedicationExists(int medicationId)
        {
            var medication = await _medicationService.GetMedicationByIdAsync(medicationId);
            return medication != null;
        }
        #endregion
        #endregion
    }
}
