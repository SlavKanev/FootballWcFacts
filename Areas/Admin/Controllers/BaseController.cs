using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static FootballWcFacts.Areas.Admin.Constants.AdminConstants;

namespace FootballWcFacts.Areas.Admin.Controllers
{
    /// <summary>
    /// Base controller with no actions
    /// </summary>
    [Area(AreaName)]
    [Route("Admin/[controller]/[Action]/{id?}")]
    [Authorize(Roles = AdminRoleName)]
    public class BaseController : Controller
    {
    }
}
