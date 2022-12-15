using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    
    public class AdminController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
