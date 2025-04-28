using HMS.API.Base;
using HMS.Core.Features.Patients.Commands.Modles;
using HMS.Core.Features.Patients.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : AppControllerBase
    {


        [HttpGet]
        //  [Authorize(Roles = "Admin,Doctors")]
        public async Task<ActionResult> GetAllPatients()
        {
            var respose = await Mediator.Send(new GetPatientListQuery());
            return NewResult(respose);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            return NewResult(await Mediator.Send(new GetPatientByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePatient(int id, UpdatePatientCommand command)
        {
            if (id != command.Id)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await Mediator.Send(new DeletePatientCommand { Id = id });
            return NewResult(result);
        }

        [HttpGet("paginated")]

        public async Task<IActionResult> Paginated([FromQuery] GetPatientPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

    }
}
