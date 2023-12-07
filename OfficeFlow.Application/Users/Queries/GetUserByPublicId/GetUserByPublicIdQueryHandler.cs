using AutoMapper;
using MediatR;
using OfficeFlow.Application.Roles.Queries.GetRoleById;
using OfficeFlow.Application.Users.Models;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Queries.GetUserByPublicId
{
    public class GetUserByPublicIdQueryHandler : IRequestHandler<GetUserByPublicIdQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetUserByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<DetailsModel> Handle(GetUserByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserByPublicId(request.PublicId);
            var mappedUser = _mapper.Map<DetailsModel>(user);
            mappedUser.Role = (await _mediator.Send(new GetRoleByIdQuery(mappedUser.RoleId))).Name;

            return mappedUser;
        }
    }
}
