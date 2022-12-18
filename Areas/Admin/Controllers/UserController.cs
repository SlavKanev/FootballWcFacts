using FootballWcFacts.Core.Contracts.Admin;
using Microsoft.AspNetCore.Mvc;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    /// <summary>
    /// User controller
    /// </summary>
    public class UserController : BaseController
    {

        private readonly IUserService userService;
        /// <summary>
        /// Dependency injection
        /// </summary>
        /// <param name="_userService"></param>
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }
        /// <summary>
        /// Gets all users
        /// </summary>
        /// <returns>view</returns>
        public async Task<IActionResult> All()
        {

            var model = await userService.All();

            return View(model);
        }
    }
}
