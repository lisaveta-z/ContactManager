using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public interface IContactService : IDisposable
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> Add(Contact newContact); 
        Task Delete(int id);
        Task Update(Contact contact);
        Task<Contact> GetById(int id); 
        Task<IEnumerable<Contact>> GetByTag(string tag);
    }
}
