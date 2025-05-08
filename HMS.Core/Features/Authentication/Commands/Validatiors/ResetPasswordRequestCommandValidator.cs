using FluentValidation;
using HMS.Core.Features.Authentication.Commands.Models;
using HMS.Core.Resources;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Authentication.Commands.Validatiors
{
    public class ResetPasswordRequestCommandValidator : AbstractValidator<ResetPasswordRequestCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        #endregion

        #region Constructors
        public ResetPasswordRequestCommandValidator(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                 .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required]);


        }

        public void ApplyCustomValidationsRules()
        {
        }

        #endregion

    }
}
