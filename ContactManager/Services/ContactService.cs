using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactManager.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactsContext _context;

        public ContactService(ContactsContext context)
        {
            _context = context;
        }

        private async Task<bool> ContactExists(int id)
        {
            return await GetById(id) != null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            return await _context.Contacts.ToListAsync();
        }

        public async Task<Contact> GetById(int id) 
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<Contact> Add(Contact newContact) 
        {
            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return newContact;
        }

        public async Task Update(Contact newContact)
        {
            var oldContact = _context.Contacts.Find(newContact.Id);
            if (oldContact == null) return;

            _context.Entry(oldContact).CurrentValues.SetValues(newContact);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toRemove = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(toRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contact>> GetByTag(string tag)
        {
            return await _context.Contacts.Where(c => c.Tag == tag).ToListAsync();
        }
    }
}
