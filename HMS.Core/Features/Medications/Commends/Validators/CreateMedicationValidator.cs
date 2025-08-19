using FluentValidation;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Commends.Validators
{
    public class CreateMedicationValidator : AbstractValidator<CreateMedicationCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public CreateMedicationValidator(
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
            RuleFor(x => x.Name)
                .MustAsync(async (name, CancellationToken) => !await _medicationService.IsMedicationNameExistsAsync(name))
                .WithMessage(_localizer[SharedResourcesKeys.MedicationNameExists]);
        }
        #endregion
    }
}
