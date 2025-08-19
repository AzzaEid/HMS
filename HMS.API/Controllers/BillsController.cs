using HMS.API.Base;
using HMS.Core.Features.Bills.Commands.Models;
using HMS.Core.Features.Bills.Queries.Models;
using HMS.Data.Entities.Enums;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : AppControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetAllBills()
        {
            return NewResult(await Mediator.Send(new GetBillListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBillById(int id)
        {
            return NewResult(await Mediator.Send(new GetBillByIdQuery { BillID = id }));
        }

        [HttpGet("status/{status}")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetBillsByStatus(BillStatus status)
        {
            return NewResult(await Mediator.Send(new GetBillsByStatusQuery { Status = status }));
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientBills(int patientId)
        {
            return NewResult(await Mediator.Send(new GetPatientBillsQuery { PatientId = patientId }));
        }

        [HttpGet("overdue")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetOverdueBills([FromQuery] int daysOverdue = 30)
        {
            return NewResult(await Mediator.Send(new GetOverdueBillsQuery { DaysOverdue = daysOverdue }));
        }

        [HttpGet("statistics")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> GetBillStatistics()
        {
            return NewResult(await Mediator.Send(new GetBillStatisticsQuery()));
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetBillsPaginated([FromQuery] GetBillsPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> CreateBill(CreateBillCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Accountant")]
        public async Task<IActionResult> UpdateBill(int id, UpdateBillCommand command)
        {
            if (id != command.BillID)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}/pay")]
        [Authorize(Roles = "Admin,Accountant,Receptionist")]
        public async Task<IActionResult> PayBill(int id)
        {
            return NewResult(await Mediator.Send(new PayBillCommand { BillID = id }));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBill(int id)
        {
            return NewResult(await Mediator.Send(new DeleteBillCommand { BillID = id }));
        }
    }
}