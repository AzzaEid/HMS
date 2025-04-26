using HMS.API.Base;
using HMS.Core.Bases;
using HMS.Core.Features.Patients.Commands.Modles;
using HMS.Core.Features.Patients.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : AppControllerBase
    {

        [HttpGet]
        //  [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult> GetAllPatients()
        {
            var respose = await Mediator.Send(new GetPatientListQuery());
            return NewResult(respose);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePatient(CreatePatientCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

    }
}
