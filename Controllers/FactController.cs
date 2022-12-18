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
    /// <summary>
    /// Fact controller
    /// </summary>
    [Authorize]
    public class FactController : Controller
    {
        private readonly IFactService factService;
        private readonly IAuthorService authorService;
        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="_factService"></param>
        /// <param name="_authorService"></param>
        public FactController(
            IFactService _factService,
            IAuthorService _authorService)
        {
            factService = _factService;
            authorService = _authorService;

        }
        /// <summary>
        /// Gets all facts
        /// </summary>
        /// <param name="query"></param>
        /// <returns>view</returns>
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
        /// <summary>
        /// Gets all mine facts
        /// </summary>
        /// <returns>view</returns>
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
        /// <summary>
        /// Gets facts details
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view</returns>
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
        /// <summary>
        /// Adds a story to the collection
        /// </summary>
        /// <returns>view</returns>
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
        /// <summary>
        /// Adds a fact
        /// </summary>
        /// <param name="model"></param>
        /// <returns>view</returns>
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
        /// <summary>
        /// Edits a fact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view</returns>
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
        /// <summary>
        /// Edits a fact story
        /// </summary>
        /// <param name="model"></param>
        /// <returns>model</returns>
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
        /// <summary>
        /// Deletes a fact
        /// </summary>
        /// <param name="id"></param>
        /// <returns>view</returns>
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
        /// <summary>
        /// Deletes a fact
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns>redirect to action</returns>
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
