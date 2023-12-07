using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OfficeFlow.Application.Dictionaries.Queries.GetAbsenceTypeDictionaries
{
    public class GetAbsenceTypeDictionariesQuery : IRequest<List<SelectListItem>?>
    {
    }
}
