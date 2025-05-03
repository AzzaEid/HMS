using HMS.Core.Bases;
using HMS.Data.Requests;
using MediatR;

namespace HMS.Core.Features.Authorization.Commands.Models
{
    public class EditRoleCommand : EditRoleRequest, IRequest<Response<string>>
    {

    }
}
