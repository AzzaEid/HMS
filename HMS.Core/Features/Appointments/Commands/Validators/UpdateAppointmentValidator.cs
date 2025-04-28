using FluentValidation;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Validators
{
    public class UpdateAppointmentValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public UpdateAppointmentValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.PatientID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.DoctorID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid appointment status.");
        }

        public void ApplyCustomValidationsRules()
        {
            // Custom rules can be added here
        }
        #endregion
    }

}
