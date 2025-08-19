using HMS.Core.Bases;
using HMS.Core.Features.Bills.Queries.Models;
using HMS.Core.Features.Bills.Queries.Results;
using HMS.Core.Resources;
using HMS.Core.Wrappers;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Bills.Queries.Hadlers
{
    public class BillQueryHandler : ResponseHandler,
          IRequestHandler<GetBillListQuery, Response<List<GetBillListResponse>>>,
          IRequestHandler<GetBillByIdQuery, Response<GetBillByIdResponse>>,
          IRequestHandler<GetBillsByStatusQuery, Response<List<GetBillListResponse>>>,
          IRequestHandler<GetPatientBillsQuery, Response<List<GetBillListResponse>>>,
          IRequestHandler<GetBillsPaginatedListQuery, PaginatedResult<GetBillListResponse>>,
          IRequestHandler<GetBillStatisticsQuery, Response<GetBillStatisticsResponse>>,
          IRequestHandler<GetOverdueBillsQuery, Response<List<GetBillListResponse>>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IBillService _billService;
        #endregion

        #region Constructors
        public BillQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IBillService billService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _billService = billService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetBillListResponse>>> Handle(GetBillListQuery request, CancellationToken cancellationToken)
        {
            var bills = await _billService.GetAllBillsAsync();
            var billListResponse = bills.Select(b => new GetBillListResponse
            {
                BillID = b.BillID,
                PrescriptionID = b.PrescriptionID,
                Amount = b.Amount,
                BillDate = b.BillDate,
                Status = b.Status,
                PatientName = b.Prescription?.Patient?.NameEn ?? string.Empty,
                DoctorName = b.Prescription?.Doctor?.NameEn ?? string.Empty,
                DaysOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid ?
                    (DateTime.Now - b.BillDate).Days : 0,
                IsOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid &&
                    (DateTime.Now - b.BillDate).Days > 30
            }).ToList();

            return Success(billListResponse);
        }

        public async Task<PaginatedResult<GetBillListResponse>> Handle(GetBillsPaginatedListQuery request, CancellationToken cancellationToken)
        {
            var query = _billService.FilterBillsPaginatedQueryable(request.OrderBy, request.Search);

            var paginatedQuery = query.Select(b => new GetBillListResponse
            {
                BillID = b.BillID,
                PrescriptionID = b.PrescriptionID,
                Amount = b.Amount,
                BillDate = b.BillDate,
                Status = b.Status,
                PatientName = b.Prescription.Patient.NameEn ?? string.Empty,
                DoctorName = b.Prescription.Doctor.NameEn ?? string.Empty,
                DaysOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid ?
                    (DateTime.Now - b.BillDate).Days : 0,
                IsOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid &&
                    (DateTime.Now - b.BillDate).Days > 30
            });

            var paginatedList = await paginatedQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return paginatedList;
        }

        public async Task<Response<GetBillStatisticsResponse>> Handle(GetBillStatisticsQuery request, CancellationToken cancellationToken)
        {
            var statistics = await _billService.GetBillStatisticsAsync();
            var overdueBills = await _billService.GetOverdueBillsAsync();

            var monthlyRevenue = await _billService.GetTotalRevenueBetweenDatesAsync(
                DateTime.Now.AddDays(-30), DateTime.Now);
            var weeklyRevenue = await _billService.GetTotalRevenueBetweenDatesAsync(
                DateTime.Now.AddDays(-7), DateTime.Now);

            var response = new GetBillStatisticsResponse
            {
                TotalBills = statistics.TotalBills,
                PaidBills = statistics.PaidBills,
                PendingBills = statistics.PendingBills,
                TotalRevenue = statistics.TotalRevenue,
                UnpaidAmount = statistics.UnpaidAmount,
                PaymentRate = statistics.PaymentRate,
                MonthlyRevenue = monthlyRevenue,
                WeeklyRevenue = weeklyRevenue,
                OverdueBillsCount = overdueBills.Count
            };

            return Success(response);
        }

        public async Task<Response<List<GetBillListResponse>>> Handle(GetOverdueBillsQuery request, CancellationToken cancellationToken)
        {
            var bills = await _billService.GetOverdueBillsAsync(request.DaysOverdue);
            var billListResponse = bills.Select(b => new GetBillListResponse
            {
                BillID = b.BillID,
                PrescriptionID = b.PrescriptionID,
                Amount = b.Amount,
                BillDate = b.BillDate,
                Status = b.Status,
                PatientName = b.Prescription?.Patient?.NameEn ?? string.Empty,
                DoctorName = b.Prescription?.Doctor?.NameEn ?? string.Empty,
                DaysOverdue = (DateTime.Now - b.BillDate).Days,
                IsOverdue = true
            }).ToList();

            return Success(billListResponse);
        }

        public async Task<Response<GetBillByIdResponse>> Handle(GetBillByIdQuery request, CancellationToken cancellationToken)
        {
            var bill = await _billService.GetBillByIdAsync(request.BillID);
            if (bill == null)
                return NotFound<GetBillByIdResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);

            var billResponse = new GetBillByIdResponse
            {
                BillID = bill.BillID,
                PrescriptionID = bill.PrescriptionID,
                Amount = bill.Amount,
                BillDate = bill.BillDate,
                Status = bill.Status,
                PatientName = bill.Prescription?.Patient?.NameEn ?? string.Empty,
                PatientAddress = bill.Prescription?.Patient?.Address ?? string.Empty,
                DoctorName = bill.Prescription?.Doctor?.NameEn ?? string.Empty,
                DoctorSpecialty = bill.Prescription?.Doctor?.Specialty?.SpecialtyName ?? string.Empty,
                PrescriptionDate = bill.Prescription?.Date ?? DateTime.MinValue,
                MedicationNames = bill.Prescription?.Medications?.Select(m => m.Name).ToList() ?? new List<string>(),
                DaysOverdue = bill.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid ?
                    (DateTime.Now - bill.BillDate).Days : 0,
                IsOverdue = bill.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid &&
                    (DateTime.Now - bill.BillDate).Days > 30
            };

            return Success(billResponse);
        }
        public async Task<Response<List<GetBillListResponse>>> Handle(GetPatientBillsQuery request, CancellationToken cancellationToken)
        {
            var bills = await _billService.GetPatientBillsAsync(request.PatientId);
            var billListResponse = bills.Select(b => new GetBillListResponse
            {
                BillID = b.BillID,
                PrescriptionID = b.PrescriptionID,
                Amount = b.Amount,
                BillDate = b.BillDate,
                Status = b.Status,
                PatientName = b.Prescription?.Patient?.NameEn ?? string.Empty,
                DoctorName = b.Prescription?.Doctor?.NameEn ?? string.Empty,
                DaysOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid ?
                    (DateTime.Now - b.BillDate).Days : 0,
                IsOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid &&
                    (DateTime.Now - b.BillDate).Days > 30
            }).ToList();

            return Success(billListResponse);
        }
        public async Task<Response<List<GetBillListResponse>>> Handle(GetBillsByStatusQuery request, CancellationToken cancellationToken)
        {
            var bills = await _billService.GetBillsByStatusAsync(request.Status);
            var billListResponse = bills.Select(b => new GetBillListResponse
            {
                BillID = b.BillID,
                PrescriptionID = b.PrescriptionID,
                Amount = b.Amount,
                BillDate = b.BillDate,
                Status = b.Status,
                PatientName = b.Prescription?.Patient?.NameEn ?? string.Empty,
                DoctorName = b.Prescription?.Doctor?.NameEn ?? string.Empty,
                DaysOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid ?
                    (DateTime.Now - b.BillDate).Days : 0,
                IsOverdue = b.Status == HMS.Data.Entities.Enums.BillStatus.Unpaid &&
                    (DateTime.Now - b.BillDate).Days > 30
            }).ToList();

            return Success(billListResponse);
        }
        #endregion
    }
}
