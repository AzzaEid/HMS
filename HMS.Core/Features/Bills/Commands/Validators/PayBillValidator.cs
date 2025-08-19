using FluentValidation;
using HMS.Core.Features.Bills.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Bills.Commands.Validators
{
    public class PayBillValidator : AbstractValidator<PayBillCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly IBillService _billService;
        #endregion

        #region Constructors
        public PayBillValidator(
            IStringLocalizer<SharedResources> localizer,
            IBillService billService)
        {
            _localizer = localizer;
            _billService = billService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.BillID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .GreaterThan(0).WithMessage(_localizer[SharedResourcesKeys.IDMustBeGreaterThanZero]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.BillID)
                .MustAsync(async (billId, CancellationToken) => await BillExists(billId))
                .WithMessage(_localizer[SharedResourcesKeys.NotFound]);

            RuleFor(x => x.BillID)
                .MustAsync(async (billId, CancellationToken) => await BillIsNotPaid(billId))
                .WithMessage(_localizer[SharedResourcesKeys.BillAlreadyPaid]);
        }

        #region Helper Methods
        private async Task<bool> BillExists(int billId)
        {
            var bill = await _billService.GetBillByIdAsync(billId);
            return bill != null;
        }

        private async Task<bool> BillIsNotPaid(int billId)
        {
            var bill = await _billService.GetBillByIdAsync(billId);
            return bill != null && bill.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid;
        }
        #endregion
        #endregion
    }
}
