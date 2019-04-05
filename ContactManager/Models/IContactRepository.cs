using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public interface IContactRepository : IDisposable
    {
        Task<Contact> AddAsync(Contact newContact); 
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id); 
        Task<List<Contact>> GetByTagAsync(string tag);
    }
}
