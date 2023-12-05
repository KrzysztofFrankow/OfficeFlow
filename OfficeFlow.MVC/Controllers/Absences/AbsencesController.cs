using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OfficeFlow.Application.Absences.Commands.CreateAbsences;
using OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept;
using OfficeFlow.Application.Users.Queries.GetAllUsers;

namespace OfficeFlow.MVC.Controllers.Absences
{
    public class AbsencesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AbsencesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAbsencesCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ListToAccept()
        {
            var list = await _mediator.Send(new GetAbsencesToAcceptQuery());
            return View(list);
        }
    }
}
