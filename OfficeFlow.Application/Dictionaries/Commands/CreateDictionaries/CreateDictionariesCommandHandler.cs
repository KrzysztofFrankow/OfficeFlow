using AutoMapper;
using MediatR;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Dictionaries.Commands.CreateDictionaries
{
    public class CreateDictionariesCommandHandler : IRequestHandler<CreateDictionariesCommand>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public CreateDictionariesCommandHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateDictionariesCommand request, CancellationToken cancellationToken)
        {
            var dictionary = _mapper.Map<Domain.Entities.Dictionaries>(request);

            await _officeFlowRepository.Create(dictionary);

            return Unit.Value;
        }
    }
}
