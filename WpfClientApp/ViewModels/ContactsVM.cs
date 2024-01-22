using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Navigation;
using WpfClientApp.Models;
using WpfClientApp.Services;
using WpfClientApp.Views;

namespace WpfClientApp.ViewModels
{
    public class ContactsVM: INotifyPropertyChanged
    {
        private readonly ContactsApi contactsApi;

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

        public ContactsVM(ContactsApi contactsApiService) // конструктор
        {
            contactsApi = contactsApiService;
        }
        
        public Contact? AddedContact { get; set; }

        private readonly AsyncRelayCommand? addContactCommand;
        public AsyncRelayCommand AddContactCommand                //добавить новый контакт
        {
            get => addContactCommand ?? new AsyncRelayCommand(async obj => 
            {

                AddedContact = new Contact();
                var contactDataWindow = new ContactDataWindow()
                {
                    DataContext = AddedContact
                };

                AddedContact.Id = Guid.NewGuid();
                var contactDataWindowResult = contactDataWindow.ShowDialog();
                if (contactDataWindowResult is false) return;

                await contactsApi.AddContact(AddedContact);
                ContactsList = await contactsApi.GetContacts(); //обновляем => event PropertyChanged  
            });
        }

        private readonly AsyncRelayCommand? changeContactCommand;
        public AsyncRelayCommand ChangeContactCommand            
        {
            get => changeContactCommand ?? new AsyncRelayCommand(async obj =>
            {
                var contactDataWindow = new ContactDataWindow()
                {
                    DataContext = SelectedContact
                };
                var contactDataWindowResult = contactDataWindow.ShowDialog();
                if (contactDataWindowResult is false) return;

                await contactsApi.ChangeContact(SelectedContact);
                ContactsList = await contactsApi.GetContacts();
            });
        }
        
        private AsyncRelayCommand deleteContact;
        public AsyncRelayCommand DeleteContact                
        {
            get => deleteContact ?? new AsyncRelayCommand(async obj =>
            {
                if (SelectedContact is null) return;

                await contactsApi.DeleteContact(SelectedContact.Id);
                ContactsList = await contactsApi.GetContacts();
            });
        }
    }
}
