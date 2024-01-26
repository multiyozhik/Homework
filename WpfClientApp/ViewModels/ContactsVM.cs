﻿using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using WpfClientApp.Models;
using WpfClientApp.Services;
using WpfClientApp.Views;

namespace WpfClientApp.ViewModels
{
    class ContactsVM : INotifyPropertyChanged //класс-DataContext для главного окна ContactsWindow
    {
        private readonly ContactsApi contactsApi;

        private readonly AuthUsersApi authUsersApi = new(new Uri(BaseRoute.baseAddress));
        public LoginVM? LoginVM { get; set; }
        public RegisterVM? RegisterVM { get; set; }

        private string? currentUserName;

        public string? CurrentUserName //отслеживаем текущего пользователя телеф.книжкой
        {
            get => currentUserName;
            set
            {
                if (value is not null)
                    currentUserName = "Welcome, " + value;
                else
                    currentUserName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(currentUserName)));
            }
        }


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
            authUsersApi = new AuthUsersApi(new Uri(BaseRoute.baseAddress));
            LoginVM = new LoginVM();
            RegisterVM = new RegisterVM();
        }

        public Contact? AddedContact { get; set; }

        private AsyncRelayCommand? addContactCommand;
        public AsyncRelayCommand AddContactCommand         //добавить новый контакт
        {
            get => addContactCommand ??= new AsyncRelayCommand(async obj =>
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

        private AsyncRelayCommand? changeContactCommand;
        public AsyncRelayCommand ChangeContactCommand
        {
            get => changeContactCommand ??= new AsyncRelayCommand(async obj =>
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
            get => deleteContact ??= new AsyncRelayCommand(async obj =>
            {
                if (SelectedContact is null) return;

                await contactsApi.DeleteContact(SelectedContact.Id);
                ContactsList = await contactsApi.GetContacts();
            });
        }

        private AsyncRelayCommand? loginCommand;
        public AsyncRelayCommand LoginCommand                //вход в систему Login
        {
            get => loginCommand ??= new AsyncRelayCommand(async obj =>
            {
                var loginWindow = new LoginWindow()
                {
                    DataContext = LoginVM
                };

                var loginWindowResult = loginWindow.ShowDialog(); //true, если заполнены поля формы
                
                if (loginWindowResult is true)
                {
                    if (await authUsersApi.Login(LoginVM))
                    {
                        CurrentUserName = LoginVM.Username;
                        MessageBox.Show($"{LoginVM.Username} - {LoginVM.Password}");
                    }                        
                    else
                        MessageBox.Show("Пользователь не найден");
                    return;
                    
                    //если сюда доходит, здесь он уже авторизован, залогинился и может пользоваться программой
                    //приветствие с именем пользователя можем показать
                    //если у нас админ - то делаем активной кнопку Список пользователей  
                }
                else
                    MessageBox.Show("Ошибка ввода");
            });
        }

        private AsyncRelayCommand? registerCommand;
        public AsyncRelayCommand RegisterCommand                //регистрация Register
        {
            get => registerCommand ??= new AsyncRelayCommand(async obj =>
            {
                var registerWindow = new RegisterWindow()
                {
                    DataContext = RegisterVM
                };

                var registerWindowResult = registerWindow.ShowDialog(); //true, если заполнены поля формы

                if (registerWindowResult is true)
                {
                    if (await authUsersApi.Register(RegisterVM))
                    {
                        CurrentUserName = RegisterVM.UserName;
                        MessageBox.Show($"{RegisterVM.UserName} - {RegisterVM.Password}");
                    }                        
                    else
                        MessageBox.Show(
                            "Ошибка ввода, пользователь с таким именем и паролем уже существует");
                    return;
                }
                else
                    MessageBox.Show("Ошибка ввода");

                //!!!В пароле обязательно заглавная и спецсимволы с цифрами

                //в RegisterVM нужно прописать валидацию - подтверждение пароля
                //https://metanit.com/sharp/wpf/14.php

            });
        }

        private AsyncRelayCommand? logoutCommand;
        public AsyncRelayCommand LogoutCommand                //выход из системы Logout
        {
            get => logoutCommand ??= new AsyncRelayCommand(async obj =>
            {
                await authUsersApi.Logout();
                CurrentUserName = null;
                return;
            });
        }

        private AsyncRelayCommand? getUsersCommand;
        public AsyncRelayCommand GetUsersCommand                //список пользователей (admin)
        {
            get => loginCommand ??= new AsyncRelayCommand(async obj =>
            {
                return;
            });
        }

    }
}