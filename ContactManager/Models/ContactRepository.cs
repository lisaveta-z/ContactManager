using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContactManager.Models
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsContext _context;

        public ContactRepository(ContactsContext context)
        {
            _context = context;
        }

        private async Task<bool> ContactExists(int id)
        {
            return await GetByIdAsync(id) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Contact>> GetAllAsync()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetByIdAsync(int id) 
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> AddAsync(Contact newContact) 
        {
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return newContact;
        }

        public async Task<bool> UpdateAsync(Contact Contact)
        {
            if (!await ContactExists(Contact.Id))
            {
                return false;
            }

            _context.Contacts.Update(Contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (!await ContactExists(id))
            {
                return false;
            }

            var toRemove = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(toRemove);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Contact>> GetByTagAsync(string tag)
        {
            return await _context.Contacts.Where(a => a.Tag == tag).ToListAsync();
        }
    }
}
