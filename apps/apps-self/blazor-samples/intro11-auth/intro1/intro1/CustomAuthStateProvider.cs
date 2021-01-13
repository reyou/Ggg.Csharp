using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace intro1
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "mrfibuli"),
            },
                "Fake authentication type"); 
            ClaimsPrincipal user = new ClaimsPrincipal(identity); 
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
