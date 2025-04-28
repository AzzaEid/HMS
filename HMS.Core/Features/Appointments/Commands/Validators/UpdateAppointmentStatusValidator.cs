using FluentValidation;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Validators
{
    public class UpdateAppointmentStatusValidator : AbstractValidator<UpdateAppointmentStatusCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public UpdateAppointmentStatusValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid appointment status.");
        }
        #endregion
    }
}


