using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationJsonMergePatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPatch("user")]
        public ActionResult<User> UpdateUser([FromBody] JsonPatchDocument<User> patchDocument)
        {
            string serializeObject = JsonConvert.SerializeObject(patchDocument);
            Console.WriteLine(serializeObject);

            User user = new User()
            {
                Id = new Guid("5B08D443-6A21-48B9-A921-11C846BBB9D2"),
                FirstName = "Existing Name",
                LastName = "Existing Last Name",
                BirthDate = new DateTimeOffset(new DateTime(2000)),
            };
            patchDocument.ApplyTo(user);

            return Ok(user);
        }
    }
}
