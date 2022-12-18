using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    /// <summary>
    /// Admin controller
    /// </summary>
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
