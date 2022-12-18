using FootballWcFacts.Core.Constants;
using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Core.Models.Author;
using FootballWcFacts.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    /// <summary>
    /// Author controller
    /// </summary>
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService authorService;
        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="_authorService"></param>
        public AuthorController(IAuthorService _authorService)
        {
            authorService = _authorService;  
        }
        /// <summary>
        /// Become an author
        /// </summary>
        /// <returns>view</returns>
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
        /// <summary>
        /// Become an author
        /// </summary>
        /// <param name="model"></param>
        /// <returns>redirect to action</returns>
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
