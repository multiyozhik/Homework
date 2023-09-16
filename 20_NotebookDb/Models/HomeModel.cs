namespace _20_NotebookDb.Models
{
    public record HomeModel(ContactsDbContext Context)
    {
        public IReadOnlyCollection<Contact> Contacts = Context.Contacts.ToList<Contact>();       
    }
}
