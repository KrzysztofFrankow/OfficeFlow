using MediatR;
using OfficeFlow.Application.Users.Models;

namespace OfficeFlow.Application.Users.Commands.EditUsers
{
    public class EditUsersCommand : CreateModel, IRequest
    {
    }
}
