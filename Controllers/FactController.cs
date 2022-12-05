using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Fact;
using FootballWcFacts.Extensions;
using FootballWcFacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            var model = new FactsQueryModel();
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details()
        {
            var model = new FactDetailsModel();
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
            var model = new FactModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FactModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction(nameof(All));
        }



    }
}
