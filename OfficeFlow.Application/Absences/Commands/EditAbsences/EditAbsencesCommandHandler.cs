using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.EditAbsences
{
    public class EditAbsencesCommandHandler : IRequestHandler<EditAbsencesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public EditAbsencesCommandHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }

        public async Task<Unit> Handle(EditAbsencesCommand request, CancellationToken cancellationToken)
        {
            var absence = await _officeFlowRepository.GetAbsenceByPublicId(request.PublicId);
            absence.UserId = request.UserId;
            absence.Type = request.Type;
            absence.From = request.From;
            absence.To = request.To;
            absence.Notes = request.Notes;
            absence.DateModified = DateTime.UtcNow;

            await _officeFlowRepository.Commit();

            return Unit.Value;
        }
    }
}
