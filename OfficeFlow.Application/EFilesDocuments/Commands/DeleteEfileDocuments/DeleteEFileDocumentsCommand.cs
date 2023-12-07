using MediatR;

namespace OfficeFlow.Application.EFilesDocuments.Commands.DeleteEfileDocuments
{
    public class DeleteEFileDocumentsCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteEFileDocumentsCommand(int id)
        {
            Id = id;
        }
    }
}
