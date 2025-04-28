using FluentValidation;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Appointments.Commands.Validators
{
    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public CreateAppointmentValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.PatientID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.DoctorID)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required]);

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
                .Must(BeAValidDate).WithMessage("Appointment date must be in the future.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid appointment status.");
        }

        public void ApplyCustomValidationsRules()
        {
            // Custom rules can be added here
        }

        private bool BeAValidDate(DateTime date)
        {
            return date > DateTime.Now;
        }
        #endregion
    }
}
