using MediatR;
using OfficeFlow.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Users.Commands.EditUsers
{
    public class EditUsersCommand : CreateModel, IRequest
    {
    }
}
