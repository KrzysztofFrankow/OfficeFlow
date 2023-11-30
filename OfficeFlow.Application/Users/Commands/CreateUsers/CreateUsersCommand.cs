using MediatR;
using OfficeFlow.Application.Users.Models;

namespace OfficeFlow.Application.Users.Commands.CreateUsers
{
    public class CreateUsersCommand : CreateModel, IRequest
    {
    }
}
