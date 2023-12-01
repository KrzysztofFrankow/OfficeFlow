using AutoMapper;
using MediatR;
using OfficeFlow.Application.EFilesDocuments.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFilesDocuments.Queries.GetEFileDocumentById
{
    public class GetEFileDocumentByIdQueryHandler : IRequestHandler<GetEFileDocumentByIdQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetEFileDocumentByIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<DetailsModel> Handle(GetEFileDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var eFile = await _officeFlowRepository.GetEFileDocumentById(request.Id);
            return _mapper.Map<DetailsModel>(eFile);
        }
    }
}
