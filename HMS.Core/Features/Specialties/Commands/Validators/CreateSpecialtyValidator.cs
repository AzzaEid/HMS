using FluentValidation;
using HMS.Core.Features.Specialties.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Specialties.Commands.Validators
{
    public class CreateSpecialtyValidator : AbstractValidator<CreateSpecialtyCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResources> _localizer;
        private readonly ISpecialtyService _specialtyService;
        #endregion

        #region Constructors
        public CreateSpecialtyValidator(
            IStringLocalizer<SharedResources> localizer,
            ISpecialtyService specialtyService)
        {
            _localizer = localizer;
            _specialtyService = specialtyService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion

        #region Handle Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.SpecialtyName)
                .NotEmpty().WithMessage(_localizer[SharedResourcesKeys.NotEmpty])
                .NotNull().WithMessage(_localizer[SharedResourcesKeys.Required])
                .MaximumLength(100).WithMessage(_localizer[SharedResourcesKeys.MaxLengthis100]);
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.SpecialtyName)
                .MustAsync(async (name, CancellationToken) => !await _specialtyService.IsSpecialtyNameExistsAsync(name))
                .WithMessage(_localizer[SharedResourcesKeys.SpecialtyNameExists]);
        }
        #endregion
    }
}
