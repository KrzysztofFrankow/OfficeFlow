using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.DeleteAbsence
{
    public class DeleteAbsenceCommandHandler : IRequestHandler<DeleteAbsenceCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public DeleteAbsenceCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(DeleteAbsenceCommand request, CancellationToken cancellationToken)
        {
            var absence = await _officeFlowRepository.GetAbsenceByPublicId(request.PublicId);

            await _officeFlowRepository.Remove(absence);

            return Unit.Value;
        }
    }
}
