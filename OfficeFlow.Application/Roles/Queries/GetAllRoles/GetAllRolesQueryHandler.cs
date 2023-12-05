using AutoMapper;
using MediatR;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Domain.Entities;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, IEnumerable<Role>>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetAllRolesQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _officeFlowRepository.GetAllRoles();
        }
    }
}
