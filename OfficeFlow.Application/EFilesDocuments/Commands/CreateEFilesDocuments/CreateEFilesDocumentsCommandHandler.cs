using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFilesDocuments.Commands.CreateEFilesDocuments
{
    public class CreateEFilesDocumentsCommandHandler : IRequestHandler<CreateEFilesDocumentsCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateEFilesDocumentsCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateEFilesDocumentsCommand request, CancellationToken cancellationToken)
        {
            var eFileDocument = _mapper.Map<Domain.Entities.EFileDocuments>(request);

            if (request.DocumentContent != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.DocumentContent.CopyToAsync(memoryStream);
                    eFileDocument.DocumentContent = memoryStream.ToArray();
                }
            }
            eFileDocument.EFileId = (await _officeFlowRepository.GetEFileByPublicId(request.EFilePublicId)).Id;
            await _officeFlowRepository.Create(eFileDocument);

            return Unit.Value;
        }
    }
}
