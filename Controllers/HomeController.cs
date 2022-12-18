using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static FootballWcFacts.Areas.Admin.Constants.AdminConstants;

namespace FootballWcFacts.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IFactService factService;
        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="_factService"></param>
        public HomeController(IFactService _factService)
        {
            factService= _factService;
        }
        /// <summary>
        /// Action index
        /// </summary>
        /// <returns>view</returns>
        public async Task<IActionResult> Index()
        {

            if (User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Admin", new { area = "Admin" });
            }
            var model = await factService.LastFiveFacts();

            return View(model);
        }
        /// <summary>
        /// Errors action
        /// </summary>
        /// <returns>view</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}