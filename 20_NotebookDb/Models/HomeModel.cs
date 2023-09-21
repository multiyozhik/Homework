using System.Diagnostics.Contracts;

namespace _20_NotebookDb.Models
{
    public record HomeModel(ContactsDbContext Context)
    {
        public IReadOnlyCollection<Contact> Contacts = Context.Contacts.ToList<Contact>();        

        public void Add(Contact contact)
        { 
            Context.Contacts.Add(contact);
            Context.SaveChanges();
        }

        public void Change(Contact newDataofChangingContact)
        {           
            Context.Contacts.Update(newDataofChangingContact);    
            Context.SaveChanges();                    
            //Contacts = Context.Contacts.ToList();     // !не забываем обновить визуальную часть, тянем список из бд
        }

        public void Delete(Guid id)
        {
            var deletingContact = Context.Contacts.FirstOrDefault((contact) => contact.Id == id);
            if (deletingContact is null) return;
            Context.Contacts.Remove(deletingContact);
            Context.SaveChanges();
            //Contacts = Context.Contacts.ToList();
        }
    }
}
