using FluentValidation;
using HMS.Core.Features.Patients.Commands.Modles;

namespace HMS.Core.Features.Patients.Commands.Validators
{
    public class AddPatientValidator : AbstractValidator<CreatePatientCommand>
    {
        #region Fields

        #endregion

        #region Constructors
        public AddPatientValidator()
        {
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2).WithMessage("Minimum length is 2 ");
            RuleFor(x => x.Age)
            .NotEmpty().WithMessage("Patient age is required.")
            .InclusiveBetween(0, 120).WithMessage("Patient age must be between 0 and 120.");

            RuleFor(x => x.Gender)
            .NotEmpty().WithMessage("Patient gender is required.");

            RuleFor(x => x.ContactNumber)
            .NotEmpty().WithMessage("Contact number is required.")
            .MaximumLength(20).WithMessage("Contact number must not exceed 20 characters.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Please enter a valid phone number.");

            RuleFor(x => x.Address)
            .MaximumLength(200).WithMessage("{PropertyValue} Address must not exceed 200 characters.");
        }

        public void ApplyCustomValidationsRules()
        {




        }

        #endregion
    }
}
