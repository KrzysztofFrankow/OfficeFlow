using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Absences.Commands.DeleteLimit
{
    public class DeleteLimitCommand : IRequest
    {
        public Guid PublicId { get; set; }
        public DeleteLimitCommand(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
