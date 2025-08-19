using HMS.API.Base;
using HMS.Core.Features.Medications.Commends.Models;
using HMS.Core.Features.Medications.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllMedications()
        {
            return NewResult(await Mediator.Send(new GetMedicationListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicationById(int id)
        {
            return NewResult(await Mediator.Send(new GetMedicationByIdQuery { MedicationId = id }));
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchMedications([FromQuery] string searchTerm)
        {
            return NewResult(await Mediator.Send(new SearchMedicationsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("low-stock")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public async Task<IActionResult> GetLowStockMedications([FromQuery] int threshold = 10)
        {
            return NewResult(await Mediator.Send(new GetLowStockMedicationsQuery { Threshold = threshold }));
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetMedicationsPaginated([FromQuery] GetMedicationPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Pharmacist")]
        public async Task<IActionResult> CreateMedication(CreateMedicationCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public async Task<IActionResult> UpdateMedication(int id, UpdateMedicationCommand command)
        {
            if (id != command.MedicationId)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}/stock")]
        [Authorize(Roles = "Admin,Pharmacist")]
        public async Task<IActionResult> UpdateMedicationStock(int id, [FromBody] int newQuantity)
        {
            var command = new UpdateMedicationStockCommand
            {
                MedicationId = id,
                NewQuantity = newQuantity
            };
            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            return NewResult(await Mediator.Send(new DeleteMedicationCommand { Id = id }));
        }
    }
}
