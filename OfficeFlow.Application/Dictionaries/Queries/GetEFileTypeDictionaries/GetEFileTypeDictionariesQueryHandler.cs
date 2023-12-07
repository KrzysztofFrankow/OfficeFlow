using MediatR;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Dictionaries.Queries.GetEFileCategoryDictionaries;
using OfficeFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Application.Dictionaries.Queries.GetEFileTypeDictionaries
{
    public class GetEFileTypeDictionariesQueryHandler : IRequestHandler<GetEFileTypeDictionariesQuery, List<SelectListItem>?>
    {
        private readonly IOfficeFlowRepository _officeFlowRepository;

        public GetEFileTypeDictionariesQueryHandler(IOfficeFlowRepository officeFlowRepository)
        {
            _officeFlowRepository = officeFlowRepository;
        }
        public async Task<List<SelectListItem>?> Handle(GetEFileTypeDictionariesQuery request, CancellationToken cancellationToken)
        {
            var list = await _officeFlowRepository.GetEFileTypeDictionaries();

            var roleSelectListItems = list.Select(type => new SelectListItem
            {
                Text = type.Name,
                Value = type.Id.ToString()
            })
            .ToList();
            roleSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz typ...", Value = "" });

            return roleSelectListItems;
        }
    }
}
