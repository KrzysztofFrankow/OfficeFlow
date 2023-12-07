using AutoMapper;
using MediatR;
using OfficeFlow.Application.Absences.Models;
using OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept
{
    public class GetAbsencesToAcceptQueryHandler : IRequestHandler<GetAbsencesToAcceptQuery, List<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAbsencesToAcceptQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<List<ListModel>> Handle(GetAbsencesToAcceptQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAbsencesToAccept();
            
            var mappedList = _mapper.Map<List<ListModel>>(list);

            for (int i = 0; i < mappedList.Count(); i++)
                mappedList[i].TypeName = (await _mediator.Send(new GetDictionariesByIdQuery(mappedList[i].Type))).Name;

            return mappedList;
        }
    }
}
