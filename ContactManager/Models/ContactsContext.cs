using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    public class ContactsContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactsContext(DbContextOptions<ContactsContext> options)
            : base(options)
        { }
    }
}
