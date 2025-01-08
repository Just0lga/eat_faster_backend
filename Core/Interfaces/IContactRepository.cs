using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IContactRepository
    {
        //Create a contact
        Task CreateContact(Contact contact);

        //Delete a contact
        Task DeleteContact(Contact contact);

        //Get a contact
        Task<Contact> GetContact(int businessId, int contactId);

        //Get all contacts async
        Task<IReadOnlyList<Contact>> GetAllContacts(int businessId);

        //Get contacts with page list
        Task<PagedList<Contact>> GetContactsWithPage(int businessId, PaginationParams paginationParams);

        //Update a contact
        Task UpdateContact(Contact contact);
    }
}
