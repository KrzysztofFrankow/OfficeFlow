using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Commands.CreateLimit
{
    public class CreateLimitCommandHandler : IRequestHandler<CreateLimitCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateLimitCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateLimitCommand request, CancellationToken cancellationToken)
        {
            var limit = _mapper.Map<Domain.Entities.Limits>(request);

            await _officeFlowRepository.Create(limit);

            return Unit.Value;
        }
    }
}
