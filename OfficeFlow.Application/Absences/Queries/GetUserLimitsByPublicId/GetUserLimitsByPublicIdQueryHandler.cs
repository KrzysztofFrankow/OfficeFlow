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

namespace OfficeFlow.Application.Absences.Queries.GetUserLimitsByPublicId
{
    public class GetUserLimitsByPublicIdQueryHandler : IRequestHandler<GetUserLimitsByPublicIdQuery, UserLimitsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetUserLimitsByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper, IMediator mediator)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<UserLimitsModel> Handle(GetUserLimitsByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var userLimits = await _officeFlowRepository.GetUserLimitsByPublicId(request.PublicId);

            var mappedUserLimits = _mapper.Map<UserLimitsModel>(userLimits);

            if (mappedUserLimits != null)
            {
                for (int i = 0; i < mappedUserLimits.Limits?.Count(); i++)
                    mappedUserLimits.Limits[i].TypeName = (await _mediator.Send(new GetDictionariesByIdQuery(mappedUserLimits.Limits[i].Type))).Name;
            }

            return mappedUserLimits!;
        }
    }
}
