using MediatR;
using OfficeFlow.Application.Users.Models;

namespace OfficeFlow.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<ListModel>>
    {
    }
}
