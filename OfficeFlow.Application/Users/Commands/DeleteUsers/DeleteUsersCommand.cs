using MediatR;

namespace OfficeFlow.Application.Users.Commands.DeleteUsers
{
    public class DeleteUsersCommand : IRequest
    {
        public Guid PublicId { get; set; }
        public DeleteUsersCommand(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
