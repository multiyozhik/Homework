using _19_Notebook.Services;

namespace _19_Notebook.Models
{
    public record HomeModel(IRepository ContactsRepository)
    {
        public IReadOnlyCollection<Contact> Contacts = ContactsRepository.Contacts;       
    }
}
