using MediatR;
using OfficeFlow.Application.EFilesDocuments.Models;

namespace OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments
{
    public class CreateEFilesDocumentsCommand : CreateModel, IRequest
    {
        public Guid EFilePublicId { get; set; }

        public CreateEFilesDocumentsCommand(Guid eFilePublicId)
        {
            EFilePublicId = eFilePublicId;
        }

        public CreateEFilesDocumentsCommand()
        {
            
        }
    }
}
