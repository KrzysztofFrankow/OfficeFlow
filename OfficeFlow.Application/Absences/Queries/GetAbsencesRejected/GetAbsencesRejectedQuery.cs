using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesRejected
{
    public class GetAbsencesRejectedQuery : IRequest<List<ListModel>>
    {
    }
}
