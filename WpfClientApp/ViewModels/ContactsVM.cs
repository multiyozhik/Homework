using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClientApp.Models;
using WpfClientApp.Views;

namespace WpfClientApp.ViewModels
{
    public class ContactsVM: INotifyPropertyChanged
    {
        private readonly ContactsDbContext dbContext;

        private List<Contact>? contactsList;
        public List<Contact>? ContactsList //отслеживаем изменения коллекции контактов
        {
            get => contactsList;
            set
            {
                contactsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContactsList)));//вызываем PropertyChanged
            }
        }

        private Contact selectedContact;
        public Contact SelectedContact //отслеживаем изменение выделенного контакта
        {
            get => selectedContact;
            set
            {
                selectedContact = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedContact)));
            }
        }
        
        public event PropertyChangedEventHandler? PropertyChanged;//реализуем INotifyPropertyChanged

        public ContactsVM(ContactsDbContext dbContext)
        {
            this.dbContext = dbContext;
            ContactsList = dbContext.Contacts.ToList();
        }
        public Contact? AddedContact { get; set; }

        private readonly RelayCommand? addContactCommand;
        public RelayCommand AddContactCommand                //добавить нового клиента
        {
            get => addContactCommand ?? new RelayCommand(obj =>
            {

                AddedContact = new Contact();
                var contactDataWindow = new ContactDataWindow()
                {
                    DataContext = AddedContact
                };

                var contactDataWindowResult = contactDataWindow.ShowDialog();
                if (contactDataWindowResult is false) return;

                dbContext.Contacts.Add(AddedContact);
                dbContext.SaveChanges();
                ContactsList = dbContext.Contacts.ToList();                
            });
        }

        private readonly RelayCommand? changeContactCommand;
        public RelayCommand ChangeContactCommand            
        {
            get => changeContactCommand ?? new RelayCommand(obj =>
            {
                var contactDataWindow = new ContactDataWindow()
                {
                    DataContext = SelectedContact
                };
                var contactDataWindowResult = contactDataWindow.ShowDialog();
                if (contactDataWindowResult is false) return;
                dbContext.Contacts.Update(SelectedContact);
                dbContext.SaveChanges();
                ContactsList = dbContext.Contacts.ToList();
            });
        }
        
        private RelayCommand deleteContact;
        public RelayCommand DeleteContact                
        {
            get => deleteContact ?? new RelayCommand(obj =>
            {
                if (SelectedContact is null) return;
                dbContext.Contacts.Remove(SelectedContact);               
                dbContext.SaveChanges();
                ContactsList = dbContext.Contacts.ToList();                
            });
        }
    }
}
