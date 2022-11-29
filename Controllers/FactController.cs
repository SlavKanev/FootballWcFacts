using FootballWcFacts.Core.Models.Fact;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    public class FactController : Controller
    {
        public IActionResult All()
        {
            var model = new AllFactsQueryModel();
            return View(model);
        }
    }
}
