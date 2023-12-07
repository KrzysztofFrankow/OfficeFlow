using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.DeleteLimit
{
    public class DeleteLimitCommandHandler : IRequestHandler<DeleteLimitCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public DeleteLimitCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(DeleteLimitCommand request, CancellationToken cancellationToken)
        {
            var limit = await _officeFlowRepository.GetLimitByPublicId(request.PublicId);

            await _officeFlowRepository.Remove(limit);

            return Unit.Value;
        }
    }
}
