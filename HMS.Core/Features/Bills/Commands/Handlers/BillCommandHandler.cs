using HMS.Core.Bases;
using HMS.Core.Features.Bills.Commands.Models;
using HMS.Core.Resources;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace HMS.Core.Features.Bills.Commands.Handlers
{
    public class BillCommandHandler : ResponseHandler,
        IRequestHandler<CreateBillCommand, Response<string>>,
        IRequestHandler<UpdateBillCommand, Response<string>>,
        IRequestHandler<PayBillCommand, Response<string>>,
        IRequestHandler<DeleteBillCommand, Response<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly IBillService _billService;
        private readonly IPrescriptionService _prescriptionService;
        #endregion

        #region Constructors
        public BillCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer,
            IMapper mapper,
            IBillService billService,
            IPrescriptionService prescriptionService) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _billService = billService;
            _prescriptionService = prescriptionService;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<string>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الوصفة
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(request.PrescriptionID);
            if (prescription == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PrescriptionNotFound]);

            // إنشاء الفاتورة
            var bill = _mapper.Map<Bill>(request);
            var createdBill = await _billService.CreateBillAsync(bill);

            return Success($"Bill created successfully with ID: {createdBill.BillID}");
        }

        public async Task<Response<string>> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الفاتورة
            var existingBill = await _billService.GetBillByIdAsync(request.BillID);
            if (existingBill == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            // التحقق من حالة الفاتورة - لا يمكن تعديل الفاتورة المدفوعة
            if (existingBill.Status == HMS.Data.Entities.Enums.BillStatus.Paid)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.CannotUpdatePaidBill]);

            // التحقق من وجود الوصفة
            var prescription = await _prescriptionService.GetPrescriptionByIdAsync(request.PrescriptionID);
            if (prescription == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.PrescriptionNotFound]);

            // تحديث الفاتورة
            var bill = _mapper.Map<Bill>(request);
            await _billService.UpdateBillAsync(bill);

            return Success("" + _sharedResources[SharedResourcesKeys.Updated]);
        }

        public async Task<Response<string>> Handle(PayBillCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الفاتورة
            var bill = await _billService.GetBillByIdAsync(request.BillID);
            if (bill == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            // التحقق من أن الفاتورة لم تُدفع بعد
            if (bill.Status == HMS.Data.Entities.Enums.BillStatus.Paid)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.BillAlreadyPaid]);

            // دفع الفاتورة
            var result = await _billService.PayBillAsync(request.BillID);
            if (!result)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.PaymentFailed]);

            return Success("" + _sharedResources[SharedResourcesKeys.BillPaidSuccessfully]);
        }

        public async Task<Response<string>> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود الفاتورة
            var bill = await _billService.GetBillByIdAsync(request.BillID);
            if (bill == null)
                return NotFound<string>(_sharedResources[SharedResourcesKeys.NotFound]);

            // حذف الفاتورة
            var result = await _billService.DeleteBillAsync(request.BillID);
            if (!result)
                return BadRequest<string>(_sharedResources[SharedResourcesKeys.CannotDeletePaidBill]);

            return Success("" + _sharedResources[SharedResourcesKeys.Deleted]);
        }
        #endregion
    }
}
