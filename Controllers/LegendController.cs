using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Legend;
using FootballWcFacts.Extensions;
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
        public async Task<IActionResult> All()
        {
            var model = new LegendsQueryModel();

            return View(model);
        }
                
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = new LegendDetailsModel();
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
            var model = new LegendModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, LegendModel model)
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
