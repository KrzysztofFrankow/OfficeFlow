using AutoMapper;
using MediatR;
using OfficeFlow.Application.Absences.Models;
using OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Absences.Queries.GetAbsenceByPublicId
{
    public class GetAbsenceByPublicIdQueryHandler : IRequestHandler<GetAbsenceByPublicIdQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetAbsenceByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<DetailsModel> Handle(GetAbsenceByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var absence = await _officeFlowRepository.GetAbsenceByPublicId(request.PublicId);

            var mappedAbsence = _mapper.Map<DetailsModel>(absence);

            mappedAbsence.TypeName = (await _mediator.Send(new GetDictionariesByIdQuery(absence.Type))).Name;

            return mappedAbsence;
        }
    }
}
