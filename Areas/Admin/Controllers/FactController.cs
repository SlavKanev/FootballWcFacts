using FootballWcFacts.Areas.Admin.Models;
using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    public class FactController : BaseController
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

        public async Task<IActionResult> Mine()
        {
            var myFacts = new MyFactsViewModel();
            var adminId = User.Id();
            var authorId = await authorService.GetAuthorId(adminId);
            myFacts.AddedFacts = await factService.AllFactsByAuthorId(authorId);

            return View(myFacts);

        }
    }
}
