using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            ContactsList = contactsApi.GetContacts().Result?.ToList(); //УБРАТЬ ИЗ КОНСТРУКТОРА!!!
            //!!!ВСЕ ВИСИТ - ЕСЛИ Я ТАК ДЕЛАЮ, Т.К. ТУТ СИНХРОННО, А В API АСИНХРОННО И БЛОКИРОВКА
            //СДЕЛАТЬ ДЛЯ ОКНА ONLOAD И ТАМ CONTACTSLIST И ПОСМОТРЕТЬ АСИНХРОНЩИНУ ДЛЯ RELAYCOMMAND
        
            
        
        
        }

        
        public Contact? AddedContact { get; set; }

        private readonly RelayCommand? addContactCommand;
        public RelayCommand AddContactCommand                //добавить новый контакт
        {
            get => addContactCommand ?? new RelayCommand(obj =>  
            {

                AddedContact = new Contact();
                var contactDataWindow = new ContactDataWindow()
                {
                    DataContext = AddedContact
                };

                AddedContact.Id = Guid.NewGuid();
                var contactDataWindowResult = contactDataWindow.ShowDialog();
                if (contactDataWindowResult is false) return;

                contactsApi.AddContact(AddedContact);
                ContactsList = contactsApi.GetContacts().Result?.ToList();                               
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

                contactsApi.ChangeContact(SelectedContact);
                ContactsList = contactsApi.GetContacts().Result?.ToList();
            });
        }
        
        private RelayCommand deleteContact;
        public RelayCommand DeleteContact                
        {
            get => deleteContact ?? new RelayCommand(obj =>
            {
                if (SelectedContact is null) return;

                contactsApi.DeleteContact(SelectedContact.Id);
                ContactsList = contactsApi.GetContacts().Result?.ToList();               
            });
        }
    }
}
