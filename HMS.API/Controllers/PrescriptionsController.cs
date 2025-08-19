using HMS.API.Base;
using HMS.Core.Features.Prescriptions.Commands.Models;
using HMS.Core.Features.Prescriptions.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : AppControllerBase
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetAllPrescriptions()
        {
            return NewResult(await Mediator.Send(new GetPrescriptionListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescriptionById(int id)
        {
            return NewResult(await Mediator.Send(new GetPrescriptionByIdQuery { Id = id }));
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientPrescriptions(int patientId)
        {
            return NewResult(await Mediator.Send(new GetPatientPrescriptionsQuery { PatientId = patientId }));
        }

        [HttpGet("doctor/{doctorId}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetDoctorPrescriptions(int doctorId)
        {
            return NewResult(await Mediator.Send(new GetDoctorPrescriptionsQuery { DoctorId = doctorId }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> CreatePrescription(CreatePrescriptionCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> UpdatePrescription(int id, UpdatePrescriptionCommand command)
        {
            if (id != command.Id)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePrescription(int id)
        {
            return NewResult(await Mediator.Send(new DeletePrescriptionCommand { Id = id }));
        }

        /* [HttpGet("paginated")]
         [Authorize(Roles = "Admin,Doctor")]
         public async Task<IActionResult> GetPaginatedPrescriptions([FromQuery] GetPrescriptionPaginatedListQuery query)
         {
             return NewResult(await Mediator.Send(query));
         }
        /*/
    }
}
