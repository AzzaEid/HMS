using HMS.Core.Features.Bills.Queries.Results;
using HMS.Core.Wrappers;
using HMS.Data.Helper.Enums;
using MediatR;

namespace HMS.Core.Features.Bills.Queries.Models
{
    public class GetBillsPaginatedListQuery : IRequest<PaginatedResult<GetBillListResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public BillOrderingEnum OrderBy { get; set; } = BillOrderingEnum.DateDesc;
        public string? Search { get; set; }
    }
}
