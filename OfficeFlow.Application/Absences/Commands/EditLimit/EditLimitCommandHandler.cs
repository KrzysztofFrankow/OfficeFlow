using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.EditLimit
{
    public class EditLimitCommandHandler : IRequestHandler<EditLimitCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditLimitCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditLimitCommand request, CancellationToken cancellationToken)
        {
            var limit = await _officeFlowRepository.GetLimitByPublicId(request.PublicId);
            limit.UserId = request.UserId;
            limit.Type = request.Type;
            limit.DaysLimit = request.DaysLimit;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
