using AutoMapper;
using MediatR;
using OfficeFlow.Application.EFiles.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFiles.Queries.GetAllEFiles
{
    public class GetAllEFilesQueryHandler : IRequestHandler<GetAllEFilesQuery, IEnumerable<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetAllEFilesQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ListModel>> Handle(GetAllEFilesQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAllEFiles();

            return _mapper.Map<IEnumerable<ListModel>>(list);
        }
    }
}
