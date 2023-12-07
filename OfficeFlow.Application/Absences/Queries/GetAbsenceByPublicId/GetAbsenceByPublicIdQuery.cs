using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetAbsenceByPublicId
{
    public class GetAbsenceByPublicIdQuery : IRequest<DetailsModel>
    {
        public Guid PublicId { get; set; }

        public GetAbsenceByPublicIdQuery(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
