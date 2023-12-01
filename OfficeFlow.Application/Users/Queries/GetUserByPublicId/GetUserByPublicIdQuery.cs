using MediatR;
using OfficeFlow.Application.Users.Models;

namespace OfficeFlow.Application.Users.Queries.GetUserByPublicId
{
    public class GetUserByPublicIdQuery : IRequest<DetailsModel>
    {
        public Guid PublicId { get; set; }

        public GetUserByPublicIdQuery(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
