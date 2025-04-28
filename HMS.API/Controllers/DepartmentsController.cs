using HMS.API.Base;
using HMS.Core.Features.Departments.Commands.Modles;
using HMS.Core.Features.Departments.Queries.Models;
using HMS.Data.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDepartments()
        {
            var response = await Mediator.Send(new GetDepartmentListQuery());
            return NewResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var response = await Mediator.Send(new GetDepartmentByIdQuery { Id = id });
            return NewResult(response);
        }

        [HttpGet("{id}/doctors")]
        public async Task<IActionResult> GetDepartmentDoctors(int id)
        {
            var response = await Mediator.Send(new GetDepartmentDoctorsQuery(id));
            return NewResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand command)
        {
            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, [FromBody] UpdateDepartmentCommand command)
        {
            if (id != command.DepartmentId)
                throw new BadRequestException("The ID in the URL does not match the ID in the request body.");

            var result = await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var result = await Mediator.Send(new DeleteDepartmentCommand { Id = id });
            return NewResult(result);
        }
    }
}
