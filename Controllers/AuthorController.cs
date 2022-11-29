using FootballWcFacts.Core.Models.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        [HttpGet]
        public IActionResult Become()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeAuthorModel model)
        {
            return RedirectToAction("All", "Fact");
        }

    }
}
