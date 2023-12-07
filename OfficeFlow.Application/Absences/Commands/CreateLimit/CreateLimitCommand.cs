using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Commands.CreateLimit
{
    public class CreateLimitCommand : CreateLimitModel, IRequest
    {
    }
}
