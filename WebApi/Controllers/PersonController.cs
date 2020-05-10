using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly AppDbContext _context;
        public PersonController(AppDbContext ctx)
        {
            this._context = ctx;

        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _context.Person.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Person person = _context.Person.Find(id);
            if(person != null)
            {
                return Ok(person);
            }
            return NotFound();
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Person person)
        {
            await _context.Person.AddAsync(person);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Person person)
        {
            Person personfromDb = _context.Person.Find(1);
            if(personfromDb == null)
            {
                return NotFound();
            }
            personfromDb.Name = person.Name;
            personfromDb.Age = person.Age;
            await _context.SaveChangesAsync();
            return Ok(personfromDb);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Person personFromDb = _context.Person.Find(id);
            if(personFromDb == null)
            {
                return NotFound();
            }
            _context.Remove(personFromDb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
