using MediatR;
using OfficeFlow.Application.EFiles.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
