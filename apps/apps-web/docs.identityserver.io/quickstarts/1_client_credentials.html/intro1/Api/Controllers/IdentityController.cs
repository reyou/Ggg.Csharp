using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// https://localhost:5001/identity
    /// </summary>
    // Specifies an attribute route on a controller.
     [Route("identity")]
    // Specifies that the class or method that this attribute is applied to requires the specified authorization.
    [Authorize]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var enumerable = from c in User.Claims select new {c.Type, c.Value};
            JsonResult jsonResult = new JsonResult(enumerable);
            return jsonResult;
        }
    }
}