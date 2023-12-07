using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Dictionaries.Models;
using OfficeFlow.Domain.Interfaces;

namespace OfficeFlow.Application.Dictionaries.Queries.GetEFileCategoryDictionaries
{
    public class GetEFileCategoryDictionariesQueryHandler : IRequestHandler<GetEFileCategoryDictionariesQuery, List<SelectListItem>?>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetEFileCategoryDictionariesQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<List<SelectListItem>?> Handle(GetEFileCategoryDictionariesQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetEFileCategoryDictionaries();

            var roleSelectListItems = list.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            })
            .ToList();
            roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz kategorię...", Value = "" });

            return roleSelectListItems;
        }
    }
}
