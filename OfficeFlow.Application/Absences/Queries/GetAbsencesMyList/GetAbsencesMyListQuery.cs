using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesMyList
{
    public class GetAbsencesMyListQuery : IRequest<List<ListModel>>
    {
        public int UserId { get; set; }
        public GetAbsencesMyListQuery(int userId)
        {
            UserId = userId;
        }
    }
}
