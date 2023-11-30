using AutoMapper;
using MediatR;
using OfficeFlow.Application.Users.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Queries.GetUserByPublicId
{
    public class GetUserByPublicIdQueryHandler : IRequestHandler<GetUserByPublicIdQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetUserByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<DetailsModel> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserByPublicId(request.PublicId);
            return _mapper.Map<DetailsModel>(user);
        }
    }
}
