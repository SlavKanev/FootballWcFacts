using FootballWcFacts.Core.Models.Fact;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Controllers
{
    [Authorize]
    public class FactController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var model = new FactsQueryModel();
            return View(model);
        }
        
        public async Task<IActionResult> Mine()
        {
            var model = new FactsQueryModel();
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details()
        {
            var model = new FactDetailsModel();
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(FactModel model)
        {
            int id = 1;

            return RedirectToAction(nameof(Details), new { id });

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = new FactModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, FactModel model)
        {
            return RedirectToAction(nameof(Details), new { id });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            return RedirectToAction(nameof(All));
        }



    }
}
