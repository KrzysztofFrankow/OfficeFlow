using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Commands.CreateAbsences
{
    public class CreateAbsencesCommand : CreateModel, IRequest
    {
    }
}
