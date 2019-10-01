using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Interfaces;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<Person> models = _personService.GetAll();

            return Ok(models);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Person model = _personService.Get(id);

            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Person model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Person person = _personService.Add(model);

            return CreatedAtAction("Get", new { id = person.Id }, person);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Person model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _personService.Update(id, model);

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}