using _19_Notebook.Models;

namespace _19_Notebook.Services
{
    public interface IRepository
    {
        public IReadOnlyCollection<Contact> Contacts { get; }
    }
}

