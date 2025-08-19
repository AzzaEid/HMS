using HMS.Core.Bases;
using HMS.Core.Features.Medications.Queries.Models;
using HMS.Core.Features.Medications.Queries.Results;
using HMS.Core.Resources;
using HMS.Core.Wrappers;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Medications.Queries.Handlers
{
    public class MedicationQueryHandler : ResponseHandler,
       IRequestHandler<GetMedicationListQuery, Response<List<GetMedicationListResponse>>>,
       IRequestHandler<GetMedicationByIdQuery, Response<GetMedicationByIdResponse>>,
       IRequestHandler<SearchMedicationsQuery, Response<List<GetMedicationListResponse>>>,
       IRequestHandler<GetLowStockMedicationsQuery, Response<List<GetMedicationListResponse>>>,
       IRequestHandler<GetMedicationPaginatedListQuery, PaginatedResult<GetMedicationListResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IMedicationService _medicationService;
        #endregion

        #region Constructors
        public MedicationQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IMedicationService medicationService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _medicationService = medicationService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetMedicationListResponse>>> Handle(GetMedicationListQuery request, CancellationToken cancellationToken)
        {
            var medications = await _medicationService.GetAllMedicationsAsync();
            var medicationListResponse = medications.Select(m => new GetMedicationListResponse
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                Quantity = m.Quantity,
                Price = m.Price,
                IsLowStock = m.Quantity <= 10,
                PrescriptionCount = m.Prescriptions?.Count ?? 0
            }).ToList();

            return Success(medicationListResponse);
        }

        public async Task<Response<GetMedicationByIdResponse>> Handle(GetMedicationByIdQuery request, CancellationToken cancellationToken)
        {
            var medication = await _medicationService.GetMedicationByIdAsync(request.MedicationId);
            if (medication == null)
                return NotFound<GetMedicationByIdResponse>(_stringLocalizer[SharedResourcesKeys.MedicationNotFound]);

            var medicationResponse = new GetMedicationByIdResponse
            {
                MedicationId = medication.MedicationId,
                Name = medication.Name,
                Quantity = medication.Quantity,
                Price = medication.Price,
                IsLowStock = medication.Quantity <= 10,
                Prescriptions = medication.Prescriptions?.Select(p => new PrescriptionInMedicationResponse
                {
                    Id = p.Id,
                    Date = p.Date,
                    PatientName = p.Patient?.NameEn ?? string.Empty,
                    DoctorName = p.Doctor?.NameEn ?? string.Empty
                }).ToList() ?? new List<PrescriptionInMedicationResponse>()
            };

            return Success(medicationResponse);
        }

        public async Task<Response<List<GetMedicationListResponse>>> Handle(SearchMedicationsQuery request, CancellationToken cancellationToken)
        {
            var medications = await _medicationService.SearchMedicationsAsync(request.SearchTerm);
            var medicationListResponse = medications.Select(m => new GetMedicationListResponse
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                Quantity = m.Quantity,
                Price = m.Price,
                IsLowStock = m.Quantity <= 10,
                PrescriptionCount = m.Prescriptions?.Count ?? 0
            }).ToList();

            return Success(medicationListResponse);
        }

        public async Task<Response<List<GetMedicationListResponse>>> Handle(GetLowStockMedicationsQuery request, CancellationToken cancellationToken)
        {
            var medications = await _medicationService.GetLowStockMedicationsAsync(request.Threshold);
            var medicationListResponse = medications.Select(m => new GetMedicationListResponse
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                Quantity = m.Quantity,
                Price = m.Price,
                IsLowStock = true,
                PrescriptionCount = m.Prescriptions?.Count ?? 0
            }).ToList();

            return Success(medicationListResponse);
        }

        public async Task<PaginatedResult<GetMedicationListResponse>> Handle(GetMedicationPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var query = _medicationService.FilterMedicationsPaginatedQueryable(request.OrderBy, request.Search);

            var paginatedQuery = query.Select(m => new GetMedicationListResponse
            {
                MedicationId = m.MedicationId,
                Name = m.Name,
                Quantity = m.Quantity,
                Price = m.Price,
                IsLowStock = m.Quantity <= 10,
                PrescriptionCount = m.Prescriptions.Count
            });

            var paginatedList = await paginatedQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }
        #endregion
    }
}

