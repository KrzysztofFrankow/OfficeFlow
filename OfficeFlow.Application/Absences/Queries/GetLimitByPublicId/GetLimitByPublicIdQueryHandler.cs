using AutoMapper;
using MediatR;
using OfficeFlow.Application.Absences.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Queries.GetLimitByPublicId
{
    public class GetLimitByPublicIdQueryHandler : IRequestHandler<GetLimitByPublicIdQuery, DetailsLimitModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetLimitByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<DetailsLimitModel> Handle(GetLimitByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var limit = await _officeFlowRepository.GetLimitByPublicId(request.PublicId);

            return _mapper.Map<DetailsLimitModel>(limit);
        }
    }
}
