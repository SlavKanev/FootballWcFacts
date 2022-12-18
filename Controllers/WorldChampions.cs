using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    /// <summary>
    /// World champions controller
    /// </summary>
    public class WorldChampions : Controller
    {
        /// <summary>
        /// Action index
        /// </summary>
        /// <returns>view</returns>
        public IActionResult Index()
        {
            return View();
        }
    }
}
