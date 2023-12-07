using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OfficeFlow.Application.Dictionaries.Queries.GetEFileCategoryDictionaries
{
    public class GetEFileCategoryDictionariesQuery : IRequest<List<SelectListItem>?>
    {
    }
}
