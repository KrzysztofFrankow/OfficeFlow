using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.EditUsers
{
    public class EditUsersCommandHandler : IRequestHandler<EditUsersCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditUsersCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditUsersCommand request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserByPublicId(request.PublicId);
            user.FirstName = request.FirstName;
            user.SecondName = request.SecondName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Email = request.Email;
            user.DateOfBirth = request.DateOfBirth;
            user.CreatedBy = request.CreatedBy;
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
