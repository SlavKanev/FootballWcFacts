using FootballWcFacts.Core.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<IActionResult> All()
        {

            var model = await userService.All();

            return View(model);
        }
    }
}
