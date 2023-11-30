using MediatR;
using OfficeFlow.Application.EFiles.Models;

namespace OfficeFlow.Application.EFiles.Queries.GetAllEFiles
{
    public class GetAllEFilesQuery : IRequest<IEnumerable<ListModel>>
    {
    }
}
