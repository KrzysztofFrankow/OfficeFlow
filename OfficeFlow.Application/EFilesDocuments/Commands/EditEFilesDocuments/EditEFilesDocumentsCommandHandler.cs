using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFilesDocuments.Commands.EditEFilesDocuments
{
    public class EditEFilesDocumentsCommandHandler : IRequestHandler<EditEFilesDocumentsCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditEFilesDocumentsCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditEFilesDocumentsCommand request, CancellationToken cancellationToken)
        {
            var eFileDocument = await _officeFlowRepository.GetEFileDocumentById(request.Id);
            eFileDocument.Category = request.Category;
            eFileDocument.Type = request.Type;
            eFileDocument.Date = request.Date;
            eFileDocument.DateFrom = request.DateFrom;
            eFileDocument.DateTo = request.DateTo;
            eFileDocument.Notes = request.Notes;
            eFileDocument.DateModified = DateTime.UtcNow;

            if(request.DocumentContent != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.DocumentContent.CopyToAsync(memoryStream);
                    eFileDocument.DocumentContent = memoryStream.ToArray();
                    eFileDocument.DocumentContentType = request.DocumentContent.ContentType;
                    eFileDocument.DocumentName = request.DocumentContent.FileName;
                }
            }

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
