using MediatR;
using OfficeFlow.Domain.Entities;

namespace OfficeFlow.Application.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQuery : IRequest<IEnumerable<Role>>
    {
    }
}
