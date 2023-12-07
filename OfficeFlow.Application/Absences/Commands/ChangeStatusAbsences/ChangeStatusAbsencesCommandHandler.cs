using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.ChangeStatusAbsences
{
    public class ChangeStatusAbsencesCommandHandler : IRequestHandler<ChangeStatusAbsencesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public ChangeStatusAbsencesCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(ChangeStatusAbsencesCommand request, CancellationToken cancellationToken)
        {
            var absence = await _officeFlowRepository.GetAbsenceByPublicId(request.PublicId);
            absence.Status = request.Status;
            absence.DateModified = DateTime.UtcNow;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
