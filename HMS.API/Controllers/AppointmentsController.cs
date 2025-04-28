using HMS.API.Base;
using HMS.Core.Bases;
using HMS.Core.Features.Appointments.Commands.Models;
using HMS.Core.Features.Appointments.Queries.Models;
using HMS.Core.Features.Appointments.Queries.Results;
using HMS.Core.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : AppControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetAppointmentPaginatedListResponse>>> GetAllAppointments(
            [FromQuery] GetAppointmentPaginatedListQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetAppointmentDetailDto>>> GetAppointmentById(int id)
        {
            return await Mediator.Send(new GetAppointmentByIdQuery { Id = id });
        }

        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<Response<List<GetAppointmentListDto>>>> GetPatientAppointments(int patientId)
        {
            return await Mediator.Send(new GetPatientAppointmentsQuery { PatientId = patientId });
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<ActionResult<Response<List<GetAppointmentListDto>>>> GetDoctorAppointments(int doctorId)
        {
            return await Mediator.Send(new GetDoctorAppointmentsQuery { DoctorId = doctorId });
        }

        [HttpPost]
        public async Task<ActionResult<Response<int>>> CreateAppointment(CreateAppointmentCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<bool>>> UpdateAppointment(int id, UpdateAppointmentCommand command)
        {
            if (id != command.Id)
                return BadRequest("The ID in the URL does not match the ID in the request body.");

            return await Mediator.Send(command);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<Response<bool>>> UpdateAppointmentStatus(int id, UpdateAppointmentStatusCommand command)
        {
            if (id != command.Id)
                return BadRequest("The ID in the URL does not match the ID in the request body.");

            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<bool>>> DeleteAppointment(int id)
        {
            return await Mediator.Send(new DeleteAppointmentCommand { Id = id });
        }
    }
}

