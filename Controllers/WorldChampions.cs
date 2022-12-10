using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    public class WorldChampions : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
