using Microsoft.AspNetCore.Authorization;

namespace Intro1.Controllers
{
    public class MinimumAgeAuthorizeAttribute : AuthorizeAttribute
    {
        const string PolicyPrefix = "MinimumAge";

        public MinimumAgeAuthorizeAttribute(int age)
        {
            Age = age;
        }

        // Get or set the Age property by manipulating the underlying Policy property
        public int Age
        {
            get
            {
                if (int.TryParse(Policy.Substring(PolicyPrefix.Length), out var age))
                {
                    return age;
                }
                return default(int);
            }
            set
            {
                Policy = $"{PolicyPrefix}{value.ToString()}";
            }
        }
    }
}