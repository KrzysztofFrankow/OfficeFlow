using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept
{
    public class GetAbsencesToAcceptQuery : IRequest<IEnumerable<ListModel>>
    {
    }
}
