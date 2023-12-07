using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFilesDocuments.Commands.DeleteEfileDocuments
{
    public class DeleteEFileDocumentsCommandHandler : IRequestHandler<DeleteEFileDocumentsCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public DeleteEFileDocumentsCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(DeleteEFileDocumentsCommand request, CancellationToken cancellationToken)
        {
            var eFileDocument = await _officeFlowRepository.GetEFileDocumentById(request.Id);

            await _officeFlowRepository.Remove(eFileDocument);

            return Unit.Value;
        }
    }
}
