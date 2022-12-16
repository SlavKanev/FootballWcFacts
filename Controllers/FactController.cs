using FootballWcFacts.Core.Constants;
using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Fact;
using FootballWcFacts.Extensions;
using FootballWcFacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static FootballWcFacts.Areas.Admin.Constants.AdminConstants;

namespace FootballWcFacts.Controllers
{
    [Authorize]
    public class FactController : Controller
    {
        private readonly IFactService factService;
        private readonly IAuthorService authorService;

        public FactController(
            IFactService _factService,
            IAuthorService _authorService)
        {
            factService = _factService;
            authorService = _authorService;

        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllFactsQueryModel query)
        {
            var result = await factService.All(
                query.Tournament,
                query.Sorting,
                query.CurrentPage,
                AllFactsQueryModel.FactsPerPage);

            query.TotalFactsCount = result.TotalFactsCount;
            query.Tournaments = await factService.AllTournamentsNames();
            query.Facts = result.Facts;

            return View(query);
        }
        
        public async Task<IActionResult> Mine()
        {

            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Mine", "Fact", new { area = AreaName });
            }

            IEnumerable<FactServiceModel> myFacts;
            var userId = User.Id();

            int authorId = await authorService.GetAuthorId(userId);
            myFacts = await factService.AllFactsByAuthorId(authorId);

            return View(myFacts);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if ((await factService.Exists(id)) == false)
            {
                TempData[MessageConstant.ErrorMessage] = "The fact doesn't exist!";
                return RedirectToAction(nameof(All));
            }

            var model = await factService.FactDetailsById(id);

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if ((await authorService.IsAlreadyAnAuthor(User.Id())) == false)
            {
                return RedirectToAction(nameof(AuthorController.Become), "Author");
            }

            var model = new FactModel()
            {
                FactTournaments = await factService.AllTournaments()
            };

            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(FactModel model)
        {
            if ((await authorService.IsAlreadyAnAuthor(User.Id())) == false)
            {
                return RedirectToAction(nameof(AuthorController.Become), "Author");
            }

            if ((await factService.TournamentExists(model.TournamentId)) == false)
            {
                ModelState.AddModelError(nameof(model.TournamentId), "Tournament doesn't exist");
            }

            if (!ModelState.IsValid)
            {
                model.FactTournaments = await factService.AllTournaments();
                return View(model);
            }

            int authorId = await authorService.GetAuthorId(User.Id());

            int id = await factService.Create(model, authorId);

            return RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await factService.Exists(id)) == false)
            {
                RedirectToAction(nameof(All));
            }
            if ((await factService.HasAuthorWithId(id, User.Id())) ==false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            var fact = await factService.FactDetailsById(id);
            var tournamentId = await factService.GetFactTournamentId(id);

            var model = new FactModel()
            {
                Id = id,
                Title = fact.Title,
                TournamentId = tournamentId,
                Description = fact.Description,
                ImageUrl = fact.ImageUrl,
                FactTournaments = await factService.AllTournaments()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FactModel model)
        {
            if ((await factService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Fact doesn't exist");
                model.FactTournaments = await factService.AllTournaments();

                return View(model);
                
            }
            if ((await factService.HasAuthorWithId(model.Id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await factService.TournamentExists(model.TournamentId)) == false)
            {
                ModelState.AddModelError(nameof(model.TournamentId), "Tournament doesn't exist!");
                model.FactTournaments = await factService.AllTournaments();

                return View(model);
            }
            if (ModelState.IsValid == false)
            {
                model.FactTournaments = await factService.AllTournaments();
                return View(model);

            }

            await factService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await factService.Exists(id)) == false)
            {
                RedirectToAction(nameof(All));
            }
            if ((await factService.HasAuthorWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            var fact = await factService.FactDetailsById(id);

            var model = new FactDetailsViewModel()
            {
                ImageURL = fact.ImageUrl,
                Title = fact.Title

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, FactDetailsViewModel model)
        {
            if ((await factService.Exists(id)) == false)
            {
                RedirectToAction(nameof(All));
            }
            if ((await factService.HasAuthorWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await factService.Delete(id);

            return RedirectToAction(nameof(All));
        }



    }
}
