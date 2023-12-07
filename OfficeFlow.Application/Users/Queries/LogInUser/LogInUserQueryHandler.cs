using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OfficeFlow.Application.Roles.Queries.GetRoleById;
using OfficeFlow.Application.Users.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Queries.LogInUser
{
    public class LogInUserQueryHandler : IRequestHandler<LogInUserQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<Domain.Entities.Users> _passwordHasher;
        private readonly IMediator _mediator;

        public LogInUserQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IPasswordHasher<Domain.Entities.Users> passwordHasher, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _mediator = mediator;
        }
        public async Task<DetailsModel> Handle(LogInUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserForLogin(request.Login.Email);

            if (user == null) return null!;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Login.Password);

            if (result == PasswordVerificationResult.Failed) return null!;

            var mappedUser = _mapper.Map<DetailsModel>(user);

            mappedUser.Role = (await _mediator.Send(new GetRoleByIdQuery(mappedUser.RoleId))).Name;

            return mappedUser;
        }
    }
}
