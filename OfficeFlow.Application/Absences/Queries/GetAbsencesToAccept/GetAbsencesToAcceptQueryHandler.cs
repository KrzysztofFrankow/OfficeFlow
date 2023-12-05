using AutoMapper;
using MediatR;
using OfficeFlow.Application.Absences.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept
{
    public class GetAbsencesToAcceptQueryHandler : IRequestHandler<GetAbsencesToAcceptQuery, IEnumerable<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetAbsencesToAcceptQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ListModel>> Handle(GetAbsencesToAcceptQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAbsencesToAccept();

            return _mapper.Map<IEnumerable<ListModel>>(list);
        }
    }
}
