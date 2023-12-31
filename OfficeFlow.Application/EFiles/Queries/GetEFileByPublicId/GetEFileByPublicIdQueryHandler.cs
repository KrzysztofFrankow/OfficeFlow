﻿using AutoMapper;
using MediatR;
using OfficeFlow.Application.EFiles.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.EFiles.Queries.GetEFileByPublicId
{
    public class GetEFileByPublicIdQueryHandler : IRequestHandler<GetEFileByPublicIdQuery, DetailsModel>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;
        private readonly IMapper _mapper;

        public GetEFileByPublicIdQueryHandler(IOfficeFlowRepository officeFlowRepository, IMapper mapper)
        {
            _officeFlowRepository = officeFlowRepository;
            _mapper = mapper;
        }
        public async Task<DetailsModel> Handle(GetEFileByPublicIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _officeFlowRepository.GetEFileByPublicId(request.PublicId);
            var mappedUser = _mapper.Map<DetailsModel>(user);

            if(mappedUser.Documents != null)
            {
                for (int i = 0; i < mappedUser.Documents.Count; i++)
                {
                    mappedUser.Documents[i].CategoryName = (await _officeFlowRepository.GetDictionaryById(mappedUser.Documents[i].Category)).Name;
                    mappedUser.Documents[i].TypeName = (await _officeFlowRepository.GetDictionaryById(mappedUser.Documents[i].Type)).Name;
                }
            }

            return mappedUser;
        }
    }
}
