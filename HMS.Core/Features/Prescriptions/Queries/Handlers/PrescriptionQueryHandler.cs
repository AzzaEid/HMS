using HMS.Core.Bases;
using HMS.Core.Features.Prescriptions.Queries.Models;
using HMS.Core.Features.Prescriptions.Queries.Results;
using HMS.Core.Resources;
using HMS.Data.Entities.Enums;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Prescriptions.Queries.Handlers
{
    public class PrescriptionQueryHandler : ResponseHandler,
        IRequestHandler<GetPrescriptionListQuery, Response<List<GetPrescriptionListResponse>>>,
        IRequestHandler<GetPrescriptionByIdQuery, Response<GetPrescriptionByIdResponse>>,
        IRequestHandler<GetPatientPrescriptionsQuery, Response<List<GetPrescriptionListResponse>>>
    //     IRequestHandler<GetDoctorPrescriptionsQuery, Response<List<GetPrescriptionListResponse>>>,
    //     IRequestHandler<GetPrescriptionPaginatedListQuery, PaginatedResult<GetPrescriptionListResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IPrescriptionService _prescriptionService;
        #endregion

        #region Constructors
        public PrescriptionQueryHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IPrescriptionService prescriptionService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _prescriptionService = prescriptionService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetPrescriptionListResponse>>> Handle(GetPrescriptionListQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _prescriptionService.GetAllPrescriptionsAsync();
            var prescriptionListResponse = prescriptions.Select(p => new GetPrescriptionListResponse
            {
                Id = p.Id,
                PatientID = p.PatientID,
                PatientNameEn = p.Patient?.NameEn ?? string.Empty,
                PatientNameAr = p.Patient?.NameAr ?? string.Empty,
                DoctorID = p.DoctorID,
                DoctorNameEn = p.Doctor?.NameEn ?? string.Empty,
                DoctorNameAr = p.Doctor?.NameAr ?? string.Empty,
                Date = p.Date,
                MedicationNames = p.Medications?.Select(m => m.Name).ToList() ?? new List<string>(),
                TotalAmount = p.Bills?.Sum(b => b.Amount) ?? 0,
                HasUnpaidBills = p.Bills?.Any(b => b.Status == BillStatus.Unpaid) ?? false
            }).ToList();

            return Success(prescriptionListResponse);
        }

        public async Task<Response<GetPrescriptionByIdResponse>> Handle(GetPrescriptionByIdQuery request, CancellationToken cancellationToken)
        {
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(request.Id);
            if (prescription == null)
                return NotFound<GetPrescriptionByIdResponse>(_stringLocalizer[SharedResourcesKeys.PrescriptionNotFound]);

            var prescriptionResponse = new GetPrescriptionByIdResponse
            {
                Id = prescription.Id,
                PatientID = prescription.PatientID,
                PatientNameEn = prescription.Patient?.NameEn ?? string.Empty,
                PatientNameAr = prescription.Patient?.NameAr ?? string.Empty,
                PatientAddress = prescription.Patient?.Address ?? string.Empty,
                DoctorID = prescription.DoctorID,
                DoctorNameEn = prescription.Doctor?.NameEn ?? string.Empty,
                DoctorNameAr = prescription.Doctor?.NameAr ?? string.Empty,
                DoctorSpecialty = prescription.Doctor?.Specialty?.SpecialtyName ?? string.Empty,
                Date = prescription.Date,
                Medications = prescription.Medications?.Select(m => new MedicationInPrescriptionResponse
                {
                    MedicationId = m.MedicationId,
                    Name = m.Name,
                    Quantity = m.Quantity,
                    Price = m.Price,
                    TotalPrice = m.Price * m.Quantity
                }).ToList() ?? new List<MedicationInPrescriptionResponse>(),
                Bills = prescription.Bills?.Select(b => new BillInPrescriptionResponse
                {
                    BillID = b.BillID,
                    Amount = b.Amount,
                    BillDate = b.BillDate,
                    Status = b.Status
                }).ToList() ?? new List<BillInPrescriptionResponse>(),
                TotalAmount = prescription.Bills?.Sum(b => b.Amount) ?? 0
            };

            return Success(prescriptionResponse);
        }

        public async Task<Response<List<GetPrescriptionListResponse>>> Handle(GetPatientPrescriptionsQuery request, CancellationToken cancellationToken)
        {
            var prescriptions = await _prescriptionService.GetPatientPrescriptionsAsync(request.PatientId);
            var prescriptionListResponse = prescriptions.Select(p => new GetPrescriptionListResponse
            {
                Id = p.Id,
                PatientID = p.PatientID,
                PatientNameEn = p.Patient?.NameEn ?? string.Empty,
                PatientNameAr = p.Patient?.NameAr ?? string.Empty,
                DoctorID = p.DoctorID,
                DoctorNameEn = p.Doctor?.NameEn ?? string.Empty,
                DoctorNameAr = p.Doctor?.NameAr ?? string.Empty,
                Date = p.Date,
                MedicationNames = p.Medications?.Select(m => m.Name).ToList() ?? new List<string>(),
                TotalAmount = p.Bills?.Sum(b => b.Amount) ?? 0,
                HasUnpaidBills = p.Bills?.Any(b => b.Status == BillStatus.Unpaid) ?? false
            }).ToList();

            return Success(prescriptionListResponse);
        }
        #endregion
    }
}
