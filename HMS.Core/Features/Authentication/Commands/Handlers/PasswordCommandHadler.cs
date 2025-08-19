
using HMS.Core.Bases;
using HMS.Core.Features.Authentication.Commands.Models;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Authentication.Commands.Handlers
{
    public class PasswordCommandHadler : ResponseHandler,
        IRequestHandler<ResetPasswordRequestCommand, Response<string>>,
        IRequestHandler<ResetPasswordCommand, Response<string>>
    {
        private readonly IAuthenticationService _authenticationService;

        public PasswordCommandHadler(IStringLocalizer<SharedResources> stringLocalizer,
            IAuthenticationService authenticationService)
            : base(stringLocalizer)
        {
            _authenticationService = authenticationService;
        }
        public async Task<Response<string>> Handle(ResetPasswordRequestCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.SendResetPasswordCode(request.Email);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "GenrateTokenField": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.GenrateTokenField]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgainInAnotherTime]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.TryAgainInAnotherTime], result);
            }
        }
        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPassword(request.Email, request.Token, request.Password);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
                case "Failed": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.InvaildCode], result);
            }
        }
    }
}
