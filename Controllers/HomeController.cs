using FootballWcFacts.Core.Contracts;
using FootballWcFacts.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FootballWcFacts.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFactService factService;

        public HomeController(IFactService _factService)
        {
            factService= _factService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await factService.LastFiveFacts();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}