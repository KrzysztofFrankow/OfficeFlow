using MediatR;
using OfficeFlow.Application.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
