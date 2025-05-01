using HMS.Core.Bases;
using HMS.Core.Features.ApplicationUser.Queries.Models;
using HMS.Core.Features.ApplicationUser.Queries.Results;
using HMS.Core.Resources;
using HMS.Core.Wrappers;
using HMS.Data.Entities.Identity;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,
        IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationReponse>>
      , IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        #endregion

        #region Handle Functions
        public async Task<PaginatedResult<GetUserPaginationReponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {

            // Use ProjectToType<T>() to map IQueryable directly in the database query,
            // fetching only needed fields and improving performance over Adapt<T>().

            var query = _userManager.Users.AsNoTracking().ProjectToType<GetUserPaginationReponse>();

            var paginatedList = await query.ToPaginatedListAsync(request.PageNumber, request.PageSize);

            return paginatedList;
        }

        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetUserByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);
        }
        #endregion
    }
}
