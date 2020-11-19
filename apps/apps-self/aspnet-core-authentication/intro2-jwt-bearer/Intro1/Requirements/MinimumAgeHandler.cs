using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Intro1.Requirements
{
    public class MinimumAgeHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            ClaimsPrincipal claimsPrincipal = context.User;
            IAuthorizationRequirement authorizationRequirement = context.Requirements.First();
            context.Succeed(authorizationRequirement);
            return Task.CompletedTask;
        }
    }
}