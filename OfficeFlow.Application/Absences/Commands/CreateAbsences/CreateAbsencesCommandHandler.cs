using AutoMapper;
using MediatR;
using OfficeFlow.Application.Enums;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.CreateAbsences
{
    public class CreateAbsencesCommandHandler : IRequestHandler<CreateAbsencesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateAbsencesCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateAbsencesCommand request, CancellationToken cancellationToken)
        {
            var absence = _mapper.Map<Domain.Entities.Absences>(request);
            absence.Status = (int)AbsenceStatus.InProgress;

            await _officeFlowRepository.Create(absence);

            return Unit.Value;
        }
    }
}
