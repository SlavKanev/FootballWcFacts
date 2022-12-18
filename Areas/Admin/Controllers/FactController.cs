using FootballWcFacts.Areas.Admin.Models;
using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    /// <summary>
    /// Fact controller
    /// </summary>
    public class FactController : BaseController
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
        /// Gets my facts
        /// </summary>
        /// <returns>view</returns>
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
