using AutoMapper;
using MediatR;
using OfficeFlow.Application.EFilesDocuments.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFilesDocuments.Queries.GetDocumentById
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, DocumentModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetDocumentByIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<DocumentModel> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var document = await _officeFlowRepository.GetDocumentById(request.Id);
            return _mapper.Map<DocumentModel>(document);
        }
    }
}
