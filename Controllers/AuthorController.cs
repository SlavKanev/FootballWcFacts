using FootballWcFacts.Core.Constants;
using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Author;
using FootballWcFacts.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService _authorService)
        {
            authorService = _authorService;  
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await authorService.IsAlreadyAnAuthor(User.Id()))
            {
                TempData[MessageConstant.SuccessMessage] = "You are already a FactAuthor!";
                return RedirectToAction("Index", "Home");
            }

            var model = new BecomeAuthorModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAuthorModel model)
        {
            var userId = User.Id();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (await authorService.IsAlreadyAnAuthor(userId))
            {
                TempData[MessageConstant.SuccessMessage] = "You are already a FactAuthor!";
                return RedirectToAction("Index", "Home");
            }

            await authorService.Create(userId, model.FavouriteTeam);

            return RedirectToAction("Add", "Fact");
        }

    }
}
