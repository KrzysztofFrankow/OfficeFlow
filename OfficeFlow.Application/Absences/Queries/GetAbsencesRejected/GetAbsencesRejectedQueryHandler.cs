using AutoMapper;
using MediatR;
using OfficeFlow.Application.Absences.Models;
using OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept;
using OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Absences.Queries.GetAbsencesRejected
{
    public class GetAbsencesRejectedQueryHandler : IRequestHandler<GetAbsencesRejectedQuery, List<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAbsencesRejectedQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<List<ListModel>> Handle(GetAbsencesRejectedQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAbsencesRejected();

            var mappedList = _mapper.Map<List<ListModel>>(list);

            for (int i = 0; i < mappedList.Count(); i++)
                mappedList[i].TypeName = (await _mediator.Send(new GetDictionariesByIdQuery(mappedList[i].Type))).Name;

            return mappedList;
        }
    }
}
