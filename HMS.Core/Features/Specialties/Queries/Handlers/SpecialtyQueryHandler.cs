using HMS.Core.Bases;
using HMS.Core.Features.Specialties.Queries.Models;
using HMS.Core.Features.Specialties.Queries.Results;
using HMS.Core.Resources;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Specialties.Queries.Handlers
{
    public class SpecialtyQueryHandler : ResponseHandler,
        IRequestHandler<GetSpecialtyListQuery, Response<List<GetSpecialtyListResponse>>>,
        IRequestHandler<GetSpecialtyByIdQuery, Response<GetSpecialtyByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly ISpecialtyService _specialtyService;
        #endregion

        #region Constructors
        public SpecialtyQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            ISpecialtyService specialtyService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _specialtyService = specialtyService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetSpecialtyListResponse>>> Handle(GetSpecialtyListQuery request, CancellationToken cancellationToken)
        {
            var specialties = await _specialtyService.GetAllSpecialtiesAsync();
            var specialtyListResponse = _mapper.Map<List<GetSpecialtyListResponse>>(specialties);
            return Success(specialtyListResponse);
        }

        public async Task<Response<GetSpecialtyByIdResponse>> Handle(GetSpecialtyByIdQuery request, CancellationToken cancellationToken)
        {
            var specialty = await _specialtyService.GetSpecialtyByIdAsync(request.SpecialtyId);
            if (specialty == null)
                return NotFound<GetSpecialtyByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var specialtyResponse = _mapper.Map<GetSpecialtyByIdResponse>(specialty);
            return Success(specialtyResponse);
        }
        #endregion
    }
}
