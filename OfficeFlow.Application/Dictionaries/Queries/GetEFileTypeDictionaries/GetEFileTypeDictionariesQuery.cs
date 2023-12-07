using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OfficeFlow.Application.Dictionaries.Queries.GetEFileTypeDictionaries
{
    public class GetEFileTypeDictionariesQuery : IRequest<List<SelectListItem>?>
    {
    }
}
