using System;

namespace ClientAppWpf
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string? FamilyName { get; set; }
        public string? Name { get; set; }
        public string? MiddleName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Adress { get; set; }
    }
}