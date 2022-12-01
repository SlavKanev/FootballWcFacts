using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace FootballWcFacts.Extensions
{
    public static class ClaimsPrincipalExtension
    {

        public static string Id(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

    }
}
