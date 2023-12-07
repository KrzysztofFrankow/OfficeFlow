using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Commands.EditLimit
{
    public class EditLimitCommand : CreateLimitModel, IRequest
    {
    }
}
