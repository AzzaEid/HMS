
using HMS.Data.Entities.Identity;
using HMS.Infrustructure.Data;
using HMS.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HMS.Service.Implementations
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Fields
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailsService _emailsService;
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IUrlHelper _urlHelper;
        #endregion
        #region Constructors
        public ApplicationUserService(UserManager<User> userManager,
                                      IHttpContextAccessor httpContextAccessor,
                                      IEmailsService emailsService,
                                      ApplicationDBContext applicationDBContext,
                                      IUrlHelper urlHelper)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _emailsService = emailsService;
            _applicationDBContext = applicationDBContext;
            _urlHelper = urlHelper;
        }
        #endregion
        #region Handle Functions
        public async Task<string> AddUserAsync(User user, string password)
        {
            var trans = await _applicationDBContext.Database.BeginTransactionAsync();
            try
            {
                //if Email is Exist
                var existUser = await _userManager.FindByEmailAsync(user.Email);
                if (existUser != null) return "EmailIsExist";

                //if username is Exist
                var userByUserName = await _userManager.FindByNameAsync(user.UserName);
                if (userByUserName != null) return "UserNameIsExist";

                //Create
                var createResult = await _userManager.CreateAsync(user, password);
                //Failed
                if (!createResult.Succeeded)
                    return string.Join(",", createResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "User");

                //Send Confirm Email
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                var resquestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = resquestAccessor.Scheme + "://" + resquestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { UserId = user.Id, Code = code });
                var message = $"To Confirm Email Click Link: <a href='{returnUrl}'>Link Of Confirmation</a>";

                //message 
                var emailResult = await _emailsService.SendEmail(user.Email, message, "ConFirm Email");
                if (emailResult != "Success")
                {
                    await trans.RollbackAsync();
                    return "EmailSendFailed";
                }

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        #endregion
    }
}
