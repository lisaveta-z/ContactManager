using System.Collections.Generic;

namespace ContactManager.Models
{
    public interface IContactRepository
    {
        void Create(Contact contact);
        void Delete(int id);
        void Update(int id, Contact contact);
        Contact Get(int id);
        IEnumerable<Contact> GetAllForTag(string tag);
    }
}
