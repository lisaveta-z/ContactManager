using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Models;
using ContactManager.Services;

namespace ContactManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _service;

        public ContactsController(IContactService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var contacts = await _service.GetAll();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var contact = await _service.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        [HttpGet("tag/{tag}")]
        public async Task<IActionResult> GetByTag([FromRoute] string tag)
        {
            var contactsByTag = await _service.GetByTag(tag);
            return Ok(contactsByTag);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contact == null)
            {
                return BadRequest();
            }

            await _service.Add(contact);

            return CreatedAtAction("Get", new { id = contact.Id }, contact);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contact.Id)
            {
                return BadRequest();
            }

            if (await _service.GetById(id) == null)
            {
                return NotFound();
            }

            await _service.Update(contact);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _service.GetById(id);

            if (contact == null)
            {
                return NotFound();
            }

            await _service.Delete(id);
            return Ok(contact);
        }
    }
}