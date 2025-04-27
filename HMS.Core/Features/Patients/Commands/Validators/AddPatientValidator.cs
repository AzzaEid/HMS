using FluentValidation;
using HMS.Core.Features.Patients.Commands.Modles;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Patients.Commands.Validators
{
    public class AddPatientValidator : AbstractValidator<CreatePatientCommand>
    {
        #region Fields
        private readonly IPatientService _patientService;
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public AddPatientValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull()
                .MinimumLength(2).WithMessage(_localizer[SharedResourcesKeys.MinLengthis2]);
            RuleFor(x => x.Age)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
            .InclusiveBetween(0, 120).WithMessage("Patient age must be between 0 and 120.");

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Patient gender is required.");

            RuleFor(x => x.ContactNumber)
            .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Required])
            .MaximumLength(20).WithMessage("Contact number must not exceed 20 characters.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please enter a valid phone number.");

            RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);
        }

        public void ApplyCustomValidationsRules()
        {




        }

        #endregion
    }
}
