using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.CreateUsers
{
    public class CreateUsersCommandHandler : IRequestHandler<CreateUsersCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Domain.Entities.Users> _passwordHasher;

        public CreateUsersCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IPasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(CreateUsersCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Domain.Entities.Users>(request);

            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password!);
            await _officeFlowRepository.Create(user);

            return Unit.Value;
        }
    }
}
