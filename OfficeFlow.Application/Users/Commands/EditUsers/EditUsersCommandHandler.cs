using MediatR;
using Microsoft.AspNetCore.Identity;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.EditUsers
{
    public class EditUsersCommandHandler : IRequestHandler<EditUsersCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IPasswordHasher<Domain.Entities.Users> _passwordHasher;

        public EditUsersCommandHandler(IOfficeFlowRepository officeFlowRepository, IPasswordHasher<Domain.Entities.Users> passwordHasher)
        {
            _officeFlowRepository = officeFlowRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Unit> Handle(EditUsersCommand request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserByPublicId(request.PublicId);
            user.FirstName = request.FirstName;
            user.SecondName = request.SecondName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            if(request.Password != null)
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            user.RoleId = request.RoleId;
            user.DateOfBirth = request.DateOfBirth;
            user.Address.Country = request.Country;
            user.Address.City = request.City;
            user.Address.PostalCode = request.PostalCode;
            user.Address.HouseNumber = request.HouseNumber;
            user.Address.ApartmentNumber = request.ApartmentNumber;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
