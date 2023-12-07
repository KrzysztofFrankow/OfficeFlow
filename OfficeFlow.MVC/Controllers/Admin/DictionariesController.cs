using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.Dictionaries.Queries.GetAllDictionaries;
using OfficeFlow.Application.Dictionaries.Commands.CreateDictionaries;
using Microsoft.AspNetCore.Mvc.Rendering;
using OfficeFlow.Application.Enums;
using OfficeFlow.Application.EFiles.Commands.EditEFiles;
using OfficeFlow.Application.EFiles.Queries.GetEFileByPublicId;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using OfficeFlow.Application.Dictionaries.Queries.GetDictionariesById;
using OfficeFlow.Application.Dictionaries.Commands.EditDictionaries;

namespace OfficeFlow.MVC.Controllers.Admin
{
    public class DictionariesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public DictionariesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var list = await _mediator.Send(new GetAllDictionariesQuery());
            return View(list);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var types = Enum.GetValues(typeof(DictionaryType))
                    .Cast<DictionaryType>()
                    .Select(t => new SelectListItem
                    {
                        Text = t.GetDescription(),
                        Value = ((int)t).ToString()
                    })
                    .ToList();

            types.Insert(0, new SelectListItem { Text = "Wybierz typ...", Value = "" });
            ViewBag.Types = new SelectList(types, "Value", "Text");

            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateDictionariesCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        [Route("Dictionaries/{id}/Edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var types = Enum.GetValues(typeof(DictionaryType))
                    .Cast<DictionaryType>()
                    .Select(t => new SelectListItem
                    {
                        Text = t.GetDescription(),
                        Value = ((int)t).ToString()
                    })
                    .ToList();

            types.Insert(0, new SelectListItem { Text = "Wybierz typ...", Value = "" });
            ViewBag.Types = new SelectList(types, "Value", "Text");

            var dictionary = await _mediator.Send(new GetDictionariesByIdQuery(id));

            var model = _mapper.Map<EditDictionariesCommand>(dictionary);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Dictionaries/{id}/Edit")]
        public async Task<IActionResult> Edit(int id, EditDictionariesCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
    }
}
