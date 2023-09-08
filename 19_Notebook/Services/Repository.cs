using _19_Notebook.Models;
using System.Collections.Generic;

namespace _19_Notebook.Services
{
    public class Repository : IRepository
    {
        public IReadOnlyCollection<Contact> Contacts { get; } = new List<Contact>()
        {
            new Contact(Guid.NewGuid(), "Петров", "Петр", "Петрович", "+79504112233", "Москва"),
            new Contact(Guid.NewGuid(), "Иванов", "Иван", "Иванович", "+79524778899", "Липецк"),
            new Contact(Guid.NewGuid(), "Захарова", "Мария", "Кузьминишна", "+79059876543", "Казань")
        };
    }
}
