using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ContactRepository : IContactRepository
    {
        private readonly DataContext _context;

        public ContactRepository(DataContext context) {
            _context = context;
        }

        //Create a contact
        public async Task CreateContact(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        //Delete a contact
        public async Task DeleteContact(Contact contact)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        //Get all contacts async
        public async Task<IReadOnlyList<Contact>> GetAllContacts(int businessId)
        {
            return await _context.Contacts.Where(b => b.BusinessId == businessId).ToListAsync();
        }

        //Get a contact async
        public async Task<Contact> GetContact(int businessId, int contactId)
        {
            return await _context.Contacts.Where(b => b.BusinessId == businessId && b.Id == contactId).FirstOrDefaultAsync();
        }

        //Get contacts with page list
        public async Task<PagedList<Contact>> GetContactsWithPage(int businessId, PaginationParams paginationParams)
        {
            var query = _context.Contacts.Where(b => b.BusinessId == businessId).OrderBy(x => x.Id).AsQueryable();
            return await PagedList<Contact>.CreateAsync(query, paginationParams.PageNumber, paginationParams.PageSize);
        }

        //Update a contact
        public Task UpdateContact(Contact contact)
        {
            _context.Contacts.Update(contact);
            return _context.SaveChangesAsync();
        }
    }
}
