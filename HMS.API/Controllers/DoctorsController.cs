using HMS.API.Base;
using HMS.Core.Features.Doctors.Commands.Modles;
using HMS.Core.Features.Doctors.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDoctors()
        {
            return NewResult(await Mediator.Send(new GetDoctorListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDoctorById(int id)
        {
            return NewResult(await Mediator.Send(new GetDoctorByIdQuery { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDoctor(CreateDoctorCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(int id, UpdateDoctorCommand command)
        {
            if (id != command.Id)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            return NewResult(await Mediator.Send(new DeleteDoctorCommand { Id = id }));
        }


    }
}
