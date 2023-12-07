using MediatR;
using OfficeFlow.Application.Dictionaries.Models;

namespace OfficeFlow.Application.Dictionaries.Queries.GetAllDictionaries
{
    public class GetAllDictionariesQuery : IRequest<IEnumerable<ListModel>>
    {
    }
}
