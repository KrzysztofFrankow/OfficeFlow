using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.CreateUsers
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateUsersCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.Users>(request);

            await _officeFlowRepository.Create(user);

            return Unit.Value;
        }
    }
}
