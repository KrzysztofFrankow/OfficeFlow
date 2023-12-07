using AutoMapper;
using MediatR;
using OfficeFlow.Application.Dictionaries.Models;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Dictionaries.Queries.GetAllDictionaries
{
    public class GetAllDictionariesQueryHandler : IRequestHandler<GetAllDictionariesQuery, IEnumerable<ListModel>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetAllDictionariesQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ListModel>> Handle(GetAllDictionariesQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAllDictionaries();

            return _mapper.Map<IEnumerable<ListModel>>(list);
        }
    }
}
