using HMS.Core.Bases;
using HMS.Data.Requests;
using MediatR;

namespace HMS.Core.Features.Authorization.Commands.Models
{
    public class UpdateUserClaimsCommand : UpdateUserClaimsRequest, IRequest<Response<string>>
    {
    }
}
