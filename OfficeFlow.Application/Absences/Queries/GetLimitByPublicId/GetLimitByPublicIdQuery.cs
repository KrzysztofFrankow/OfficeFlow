using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetLimitByPublicId
{
    public class GetLimitByPublicIdQuery : IRequest<DetailsLimitModel>
    {
        public Guid PublicId { get; set; }

        public GetLimitByPublicIdQuery(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
