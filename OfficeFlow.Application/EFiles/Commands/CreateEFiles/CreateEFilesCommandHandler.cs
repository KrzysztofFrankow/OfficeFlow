using AutoMapper;
using MediatR;
using OfficeFlow.Application.Users.Commands.CreateUsers;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFiles.Commands.CreateEFiles
{
    public class CreateEFilesCommandHandler : IRequestHandler<CreateEFilesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateEFilesCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateEFilesCommand request, CancellationToken cancellationToken)
        {
            var eFile = _mapper.Map<Domain.Entities.EFiles>(request);

            await _officeFlowRepository.Create(eFile);

            return Unit.Value;
        }
    }
}
