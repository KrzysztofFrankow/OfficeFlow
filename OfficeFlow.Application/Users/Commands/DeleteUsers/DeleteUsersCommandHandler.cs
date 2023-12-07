using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Users.Commands.DeleteUsers
{
    public class DeleteUsersCommandHandler : IRequestHandler<DeleteUsersCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public DeleteUsersCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(DeleteUsersCommand request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetUserByPublicId(request.PublicId);

            await _officeFlowRepository.Remove(user);

            return Unit.Value;
        }
    }
}
