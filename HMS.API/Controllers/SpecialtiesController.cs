using HMS.API.Base;
using HMS.Core.Features.Specialties.Commands.Models;
using HMS.Core.Features.Specialties.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllSpecialties()
        {
            return NewResult(await Mediator.Send(new GetSpecialtyListQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialtyById(int id)
        {
            return NewResult(await Mediator.Send(new GetSpecialtyByIdQuery { SpecialtyId = id }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSpecialty(CreateSpecialtyCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSpecialty(int id, UpdateSpecialtyCommand command)
        {
            if (id != command.SpecialtyId)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            return NewResult(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            return NewResult(await Mediator.Send(new DeleteSpecialtyCommand { SpecialtyId = id }));
        }
    }

}
