using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesAccepted
{
    public class GetAbsencesAcceptedQuery : IRequest<List<ListModel>>
    {
    }
}
