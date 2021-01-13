using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace intro1
{
    public class CustomAuthorizationService : IAuthorizationService
    {
        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, IEnumerable<IAuthorizationRequirement> requirements)
        {
            AuthorizationResult authorizationResult = AuthorizationResult.Success();
            return await Task.FromResult(authorizationResult);
        }

        public async Task<AuthorizationResult> AuthorizeAsync(ClaimsPrincipal user, object? resource, string policyName)
        {
            throw new NotImplementedException();
        }
    }
}