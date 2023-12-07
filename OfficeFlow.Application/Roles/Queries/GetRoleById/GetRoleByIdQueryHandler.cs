using AutoMapper;
using MediatR;
using OfficeFlow.Application.Users.Queries.GetUserByPublicId;
using OfficeFlow.Domain.Entities;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Role?>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetRoleByIdQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<Role?> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            return await _officeFlowRepository.GetRoleById(request.Id);
        }
    }
}
