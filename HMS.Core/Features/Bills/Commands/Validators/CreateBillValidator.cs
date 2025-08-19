using FluentValidation;
using HMS.Core.Features.Bills.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Bills.Commands.Validators
{
    public class CreateBillValidator : AbstractValidator<CreateBillCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IPrescriptionService _prescriptionService;
        #endregion

        #region Constructors
        public CreateBillValidator(
            IStringLocalizer<SharedResources> localizer,
            IPrescriptionService prescriptionService)
        {
            _localizer = localizer;
            _prescriptionService = prescriptionService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.PrescriptionID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.IDMustBeGreaterThanZero]);

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.AmountMustBeGreaterThanZero]);

            RuleFor(x => x.BillDate)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .LessThanOrEqualTo(DateTime.Now).WithMessage(_localizer[SharedResourcesKeys.BillDateCannotBeFuture]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.PrescriptionID)
                .MustAsync(async (prescriptionId, CancellationToken) => await PrescriptionExists(prescriptionId))
                .WithMessage(_localizer[SharedResourcesKeys.PrescriptionNotFound]);
        }

        #region Helper Methods
        private async Task<bool> PrescriptionExists(int prescriptionId)
        {
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(prescriptionId);
            return prescription != null;
        }
        #endregion
        #endregion
    }
}
