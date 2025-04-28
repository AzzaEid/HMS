using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Queries.Models;
using HMS.Core.Features.Appointments.Queries.Results;
using HMS.Core.Resources;
using HMS.Core.Wrappers;
using HMS.Data.Entities;
using HMS.Service.Abstracts;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace HMS.Core.Features.Appointments.Queries.Handlers
{
    public class GetAppointmentPaginatedListHandler : ResponseHandler, IRequestHandler<GetAppointmentPaginatedListQuery, PaginatedResult<GetAppointmentPaginatedListResponse>>
    {
        private readonly IAppointmentService _appointmentService;

        public GetAppointmentPaginatedListHandler(IAppointmentService appointmentService, IStringLocalizer<SharedResources> stringLocalizer) : base(stringLocalizer)
        {
            _appointmentService = appointmentService;
        }

        public async Task<PaginatedResult<GetAppointmentPaginatedListResponse>> Handle(GetAppointmentPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Appointment, GetAppointmentPaginatedListResponse>> expression = a => new GetAppointmentPaginatedListResponse(
                a.Id,
                a.Patient.Localize(a.Patient.NameAr, a.Patient.NameEn),
                a.Doctor.Localize(a.Doctor.NameAr, a.Doctor.NameEn),
                a.Date,
                a.Status.ToString()
            );

            var filter = _appointmentService.FilterAppointmentPaginatedQuerable(request.OrderBy, request.Search);
            var pagedList = await filter.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return pagedList;
        }
    }

}
