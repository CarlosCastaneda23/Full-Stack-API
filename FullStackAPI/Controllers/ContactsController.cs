using FullStackAPI.Data;
using FullStackAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FullStackAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public ContactsController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }


        [HttpGet]
        public async Task <IActionResult> GetAllContacts()
        {
          var contacts =  await _fullStackDbContext.Contacts.ToListAsync();

            return Ok(contacts);
        }

        [HttpPost]
        public async Task <IActionResult> AddContact([FromBody] Contact contactRequest)
        {
            contactRequest.Id = Guid.NewGuid();

           await _fullStackDbContext.Contacts.AddAsync(contactRequest);
           await _fullStackDbContext.SaveChangesAsync();
                
           return Ok(contactRequest);
        }

        [HttpGet]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> GetContact([FromRoute] Guid Id)
        {
           var contact = await _fullStackDbContext.Contacts.FirstOrDefaultAsync(x => x.Id == Id);

            if (contact == null)  {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> UpdateContact([FromRoute] Guid Id, Contact updateContactRequest)
        {
           var contact = await _fullStackDbContext.Contacts.FindAsync(Id);

            if (contact == null)
            {
                return NotFound();
            }
            contact.FirstName = updateContactRequest.FirstName;
            contact.LastName = updateContactRequest.LastName;
            contact.PhoneNumber = updateContactRequest.PhoneNumber;
            contact.TextComment = updateContactRequest.TextComment;

           await _fullStackDbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid Id)
        {
            var contact = await _fullStackDbContext.Contacts.FindAsync(Id);

            if (contact == null)
            {
                return NotFound();
            }

            _fullStackDbContext.Contacts.Remove(contact);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
