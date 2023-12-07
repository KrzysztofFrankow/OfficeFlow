using MediatR;

namespace OfficeFlow.Application.EFiles.Commands.DeleteEFiles
{
    public class DeleteEFilesCommand : IRequest
    {
        public Guid PublicId { get; set; }
        public DeleteEFilesCommand(Guid publicId)
        {
            PublicId = publicId;
        }
    }
}
