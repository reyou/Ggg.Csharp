using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace intro1
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            List<IAuthorizationRequirement> requirements = new List<IAuthorizationRequirement>
            {
                new ClaimsAuthorizationRequirement(ClaimTypes.Name, null)
            };
            IEnumerable<string> schemes = new List<string>();
            AuthorizationPolicy result = new AuthorizationPolicy(requirements, schemes);
            return await Task.FromResult(result);
        }

        public async Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            throw new NotImplementedException();
        }
    }
}