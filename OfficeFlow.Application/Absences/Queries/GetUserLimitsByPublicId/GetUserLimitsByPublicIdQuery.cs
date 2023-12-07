using MediatR;
using OfficeFlow.Application.Absences.Models;

namespace OfficeFlow.Application.Absences.Queries.GetUserLimitsByPublicId
{
    public class GetUserLimitsByPublicIdQuery : IRequest<UserLimitsModel>
    {
        public Guid PublicId { get; set; }
        public GetUserLimitsByPublicIdQuery(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
