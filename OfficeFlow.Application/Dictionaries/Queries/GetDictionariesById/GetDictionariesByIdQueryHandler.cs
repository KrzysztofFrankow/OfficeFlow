using MediatR;
using OfficeFlow.Application.Roles.Queries.GetRoleById;
using OfficeFlow.Domain.Entities;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById
{
    public class GetDictionariesByIdQueryHandler : IRequestHandler<GetDictionariesByIdQuery, Domain.Entities.Dictionaries?>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetDictionariesByIdQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<Domain.Entities.Dictionaries?> Handle(GetDictionariesByIdQuery request, CancellationToken cancellationToken)
        {
            return await _officeFlowRepository.GetDictionaryById(request.Id);
        }
    }
}
