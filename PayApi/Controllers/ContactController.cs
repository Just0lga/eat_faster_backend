using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        //GET: api/contacts/{businessId}/all
        [HttpGet("{businessId}/all")]
        public async Task<ActionResult<IReadOnlyList<Contact>>> GetAllContacts(int businessId)
        {
            var contacts = await _contactRepository.GetAllContacts(businessId);
            var sortedContacts = contacts.OrderBy(x => x.Id).ToList();
            return Ok(sortedContacts);
        }

        //GET: api/contact/{businessId}/{contactId}
        [HttpGet("{businessId}/{contactId}")]
        public async Task<ActionResult<Contact>> GetContact(int businessId, int contactId)
        {
            return await _contactRepository.GetContact(businessId, contactId);
        }

        //Get contacts with page list
        [HttpGet("paged")]
        public async Task<ActionResult<PagedList<Contact>>> GetContacts (int businessId, [FromQuery] PaginationParams paginationParams)
        {
            var pagedContacts = await _contactRepository.GetContactsWithPage(businessId, paginationParams);
            Response.Headers.Append("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                pagedContacts.TotalCount,
                pagedContacts.PageSize,
                pagedContacts.CurrentPage,
                pagedContacts.TotalPages
            }));
            return Ok(pagedContacts);
        }

        //POST: api/contact
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest("Contact data cannot be null.");

            await _contactRepository.CreateContact(contact);
            return Ok(contact);
        }

        // PUT: api/contact/{id}/{businessId}/{contactId}
        [HttpPut("{businessId}/{contactId}")]
        public async Task<ActionResult<Contact>> UpdateContact(int businessId, int contactId, [FromBody] Contact newContact)
        {
            if (newContact == null)
                return BadRequest("Contact data cannot be null.");

            if (contactId != newContact.Id)
                return BadRequest();


            var oldContact = await _contactRepository.GetContact(businessId, contactId);

            if (oldContact == null)
                return NotFound();

            oldContact.ContactName = newContact.ContactName;
            oldContact.ContactDescription = newContact.ContactDescription;
            oldContact.ContactPhone = newContact.ContactPhone;
            oldContact.ContactEmail = newContact.ContactEmail;

            await _contactRepository.UpdateContact(oldContact);
            return NoContent();
        }

        [HttpDelete("{businessId}/{contactId}")]
        public async Task<ActionResult<Contact>> DeleteContact(int businessId, int contactId)
        {
            var contact = await _contactRepository.GetContact(businessId, contactId);
            if (contact == null)
                return NotFound();
            await _contactRepository.DeleteContact(contact);    
            return NoContent();
        }
    }
}
