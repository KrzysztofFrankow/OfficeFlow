using MediatR;
using OfficeFlow.Application.EFiles.Models;

namespace OfficeFlow.Application.EFiles.Queries.GetEFileByPublicId
{
    public class GetEFileByPublicIdQuery : IRequest<DetailsModel>
    {
        public Guid PublicId { get; set; }

        public GetEFileByPublicIdQuery(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
