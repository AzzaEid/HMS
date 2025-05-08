using FluentValidation;
using HMS.Core.Features.Authentication.Commands.Models;
using HMS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Authentication.Commands.Validatiors
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ResetPasswordValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);
            RuleFor(x => x.ConfirmPassword)
                 .Equal(x => x.Password).WithMessage(_localizer[SharedResourcesKeys.PasswordNotEqualConfirmPass]);

        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}
