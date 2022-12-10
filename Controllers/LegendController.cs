using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Fact;
using FootballWcFacts.Core.Models.Legend;
using FootballWcFacts.Core.Services;
using FootballWcFacts.Extensions;
using FootballWcFacts.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    [Authorize]
    public class LegendController : Controller
    {

        private readonly ILegendService legendService;
        private readonly IAuthorService authorService;

        public LegendController(
            ILegendService _legendService,
            IAuthorService _authorService)
        {
            legendService = _legendService;
            authorService = _authorService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllLegendsQueryModel query)
        {
            var result = await legendService.All(
                query.Position,
                query.Sorting,
                query.CurrentPage,
                AllLegendsQueryModel.LegendsPerPage);

            query.TotalLegendsCount = result.TotalLegendsCount;
            query.Positions = await legendService.AllPositionsNames();
            query.Legends = result.Legends;

            return View(query);
        }
                
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            if ((await legendService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await legendService.LegendDetailsById(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if ((await authorService.IsAlreadyAnAuthor(User.Id())) == false)
            {
                return RedirectToAction(nameof(AuthorController.Become), "Author");
            }
            var model = new LegendModel()
            {
                LegendPositions = await legendService.AllPositions()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LegendModel model)
        {
            if ((await authorService.IsAlreadyAnAuthor(User.Id())) == false)
            {
                return RedirectToAction(nameof(AuthorController.Become), "Author");
            }

            if ((await legendService.PositionExists(model.PositionId)) == false)
            {
                ModelState.AddModelError(nameof(model.PositionId), "Position doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                model.LegendPositions= await legendService.AllPositions();

                return View(model);
            }

            int authorId = await authorService.GetAuthorId(User.Id());

            int id = await legendService.Create(model, authorId);


            return RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if ((await legendService.Exists(id)) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if ((await legendService.HasAuthorWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            var legend = await legendService.LegendDetailsById(id);
            var positionId = await legendService.GetLegendPositionId(id);

            var model = new LegendModel()
            { 
                Id = id,
                Description= legend.Description,
                FirstName= legend.FirstName,
                LastName = legend.LastName,
                ImageUrl= legend.ImageUrl,
                Nationality = legend.Nationality,
                LegendPositions = await legendService.AllPositions()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LegendModel model)
        {
            if ((await legendService.Exists(model.Id)) == false)
            {
                ModelState.AddModelError("", "Legend doesn't exist");
                model.LegendPositions = await legendService.AllPositions();

                return View(model);

            }
            if ((await legendService.HasAuthorWithId(model.Id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            if ((await legendService.PositionExists(model.PositionId)) == false)
            {
                ModelState.AddModelError(nameof(model.PositionId), "Position doesn't exist!");
                model.LegendPositions = await legendService.AllPositions();

                return View(model);
            }
            if (ModelState.IsValid == false)
            {
                model.LegendPositions = await legendService.AllPositions();
                return View(model);
            }

            await legendService.Edit(model.Id, model);

            return RedirectToAction(nameof(Details), new { model.Id });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if ((await legendService.Exists(id)) == false)
            {
                RedirectToAction(nameof(All));
            }
            if ((await legendService.HasAuthorWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }
            var legend = await legendService.LegendDetailsById(id);

            var model = new LegendDetailsViewModel()
            {
                FirstName= legend.FirstName,
                LastName= legend.LastName,
                ImageUrl= legend.ImageUrl

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, LegendDetailsViewModel model)
        {
            if ((await legendService.Exists(id)) == false)
            {
                RedirectToAction(nameof(All));
            }
            if ((await legendService.HasAuthorWithId(id, User.Id())) == false)
            {
                return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });
            }

            await legendService.Delete(id);

            return RedirectToAction(nameof(All));
        }


    }
}
