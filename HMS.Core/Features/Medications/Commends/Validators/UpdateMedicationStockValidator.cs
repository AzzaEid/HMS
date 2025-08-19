using FluentValidation;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Commends.Validators
{
    public class UpdateMedicationStockValidator : AbstractValidator<UpdateMedicationStockCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public UpdateMedicationStockValidator(
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

            RuleFor(x => x.NewQuantity)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThanOrEqualTo(0).WithMessage(_localizer[SharedResourcesKeys.QuantityMustBeGreaterThanOrEqualToZero]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.MedicationId)
                .MustAsync(async (medicationId, CancellationToken) => await MedicationExists(medicationId))
                .WithMessage(_localizer[SharedResourcesKeys.MedicationNotFound]);
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
