using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Dictionaries.Queries.GetEFileCategoryDictionaries;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Dictionaries.Queries.GetAbsenceTypeDictionaries
{
    public class GetAbsenceTypeDictionariesQueryHandler : IRequestHandler<GetAbsenceTypeDictionariesQuery, List<SelectListItem>?>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetAbsenceTypeDictionariesQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<List<SelectListItem>?> Handle(GetAbsenceTypeDictionariesQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetAbsenceTypeDictionaries();

            var roleSelectListItems = list.Select(category => new SelectListItem
            {
                Text = category.Name,
                Value = category.Id.ToString()
            })
            .ToList();
            roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz typ...", Value = "" });

            return roleSelectListItems;
        }
    }
}
