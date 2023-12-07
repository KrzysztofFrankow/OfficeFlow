using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeFlow.Application.Absences.Commands.ChangeStatusAbsences;
using OfficeFlow.Application.Absences.Commands.CreateAbsences;
using OfficeFlow.Application.Absences.Commands.CreateLimit;
using OfficeFlow.Application.Absences.Commands.DeleteAbsence;
using OfficeFlow.Application.Absences.Commands.DeleteLimit;
using OfficeFlow.Application.Absences.Commands.EditAbsences;
using OfficeFlow.Application.Absences.Commands.EditLimit;
using OfficeFlow.Application.Absences.Queries.CheckLimitForValidator;
using OfficeFlow.Application.Absences.Queries.GetAbsenceByPublicId;
using OfficeFlow.Application.Absences.Queries.GetAbsencesAccepted;
using OfficeFlow.Application.Absences.Queries.GetAbsencesMyList;
using OfficeFlow.Application.Absences.Queries.GetAbsencesRejected;
using OfficeFlow.Application.Absences.Queries.GetAbsencesToAccept;
using OfficeFlow.Application.Absences.Queries.GetLimitByPublicId;
using OfficeFlow.Application.Absences.Queries.GetUserLimitsByPublicId;
using OfficeFlow.Application.Dictionaries.Queries.GetAbsenceTypeDictionaries;
using OfficeFlow.Application.Enums;
using OfficeFlow.Application.Users.Queries.GetAllUsers;
using System.Security.Claims;

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

        [Authorize(Roles = "Admin, Manager, User")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Create()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            var userSelectListItems = users.Select(user => new SelectListItem
            {
                Text = user.FirstName + " " + user.LastName,
                Value = user.Id.ToString()
            })
            .ToList();

            userSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz pracownika...", Value = "" });
            ViewBag.Users = new SelectList(userSelectListItems, "Value", "Text");

            var types = await _mediator.Send(new GetAbsenceTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Create(CreateAbsencesCommand command)
        {
            var isLimitExceeded = await _mediator.Send(new CheckLimitForValidatorQuery(command.UserId, command.Type, command.From, command.To, false));

            if (!isLimitExceeded)
            {
                ModelState.AddModelError("", "Limit na wybrany typ nieobecności został przekroczony.");
                return View(command);
            }

            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ListToAccept()
        {
            var list = await _mediator.Send(new GetAbsencesToAcceptQuery());
            return View(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ListAccepted()
        {
            var list = await _mediator.Send(new GetAbsencesAcceptedQuery());
            return View(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> ListRejected()
        {
            var list = await _mediator.Send(new GetAbsencesRejectedQuery());
            return View(list);
        }

        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> MyList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var list = await _mediator.Send(new GetAbsencesMyListQuery(Convert.ToInt32(userId)));
            return View(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Limits()
        {
            var list = await _mediator.Send(new GetAllUsersQuery());
            return View(list);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Accept(Guid publicId)
        {
            await _mediator.Send(new ChangeStatusAbsencesCommand(publicId, (int)AbsenceStatus.Accepted));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Reject(Guid publicId)
        {
            await _mediator.Send(new ChangeStatusAbsencesCommand(publicId, (int)AbsenceStatus.Rejected));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> BackToInProgress(Guid publicId)
        {
            await _mediator.Send(new ChangeStatusAbsencesCommand(publicId, (int)AbsenceStatus.InProgress));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }

        [Authorize(Roles = "Admin, Manager, User")]
        [Route("Absences/{publicId}/Details")]
        public async Task<IActionResult> Details(Guid publicId)
        {
            var user = await _mediator.Send(new GetAbsenceByPublicIdQuery(publicId));

            return View(user);
        }

        [Authorize(Roles = "Admin, Manager, User")]
        [Route("Absences/{publicId}/Edit")]
        public async Task<IActionResult> Edit(Guid publicId)
        {
            var absence = await _mediator.Send(new GetAbsenceByPublicIdQuery(publicId));

            var model = _mapper.Map<EditAbsencesCommand>(absence);

            var users = await _mediator.Send(new GetAllUsersQuery());

            var userSelectListItems = users.Select(user => new SelectListItem
            {
                Text = user.FirstName + " " + user.LastName,
                Value = user.Id.ToString()
            })
            .ToList();

            userSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz pracownika...", Value = "" });
            ViewBag.Users = new SelectList(userSelectListItems, "Value", "Text");

            var types = await _mediator.Send(new GetAbsenceTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager, User")]
        [HttpPost]
        [Route("Absences/{publicId}/Edit")]
        public async Task<IActionResult> Edit(Guid publicId, EditAbsencesCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Manager")]
        [Route("Limits/{publicId}/Details")]
        public async Task<IActionResult> UserLimits(Guid publicId)
        {
            var user = await _mediator.Send(new GetUserLimitsByPublicIdQuery(publicId));

            return View(user);
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateLimit(int id)
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            var userSelectListItems = users.Select(user => new SelectListItem
            {
                Text = user.FirstName + " " + user.LastName,
                Value = user.Id.ToString()
            })
            .ToList();

            userSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz pracownika...", Value = "" });
            ViewBag.Users = new SelectList(userSelectListItems, "Value", "Text");

            var types = await _mediator.Send(new GetAbsenceTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            var model = new CreateLimitCommand
            {
                UserId = id
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CreateLimit(CreateLimitCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Manager")]
        [Route("Limit/{publicId}/Edit")]
        public async Task<IActionResult> EditLimit(Guid publicId)
        {
            var limit = await _mediator.Send(new GetLimitByPublicIdQuery(publicId));

            var model = _mapper.Map<EditLimitCommand>(limit);

            var users = await _mediator.Send(new GetAllUsersQuery());

            var userSelectListItems = users.Select(user => new SelectListItem
            {
                Text = user.FirstName + " " + user.LastName,
                Value = user.Id.ToString()
            })
            .ToList();

            userSelectListItems.Insert(0, new SelectListItem { Text = "Wybierz pracownika...", Value = "" });
            ViewBag.Users = new SelectList(userSelectListItems, "Value", "Text");

            var types = await _mediator.Send(new GetAbsenceTypeDictionariesQuery());
            ViewBag.Types = new SelectList(types, "Value", "Text");

            return View(model);
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        [Route("Limit/{publicId}/Edit")]
        public async Task<IActionResult> EditLimit(Guid publicId, EditLimitCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteLimit(Guid publicId)
        {
            await _mediator.Send(new DeleteLimitCommand(publicId));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }

        [Authorize(Roles = "Admin, Manager, User")]
        public async Task<IActionResult> Delete(Guid publicId)
        {
            await _mediator.Send(new DeleteAbsenceCommand(publicId));

            var referer = Request.Headers["Referer"].ToString();

            if (!string.IsNullOrEmpty(referer)) return Redirect(referer);

            return RedirectToAction("Index", "Absences");
        }
    }
}
