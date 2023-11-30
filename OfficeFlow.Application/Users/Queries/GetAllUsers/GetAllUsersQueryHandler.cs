using AutoMapper;
using MediatR;
using OfficeFlow.Application.Users.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ListModel>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAllUsers();

            return _mapper.Map<IEnumerable<ListModel>>(list);
        }
    }
}
