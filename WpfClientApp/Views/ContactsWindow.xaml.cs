using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using WpfClientApp.Services;
using WpfClientApp.ViewModels;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace WpfClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        private readonly ContactsApi contactsApi;
        public ContactsWindow()
        {
            InitializeComponent();
            contactsApi = new ContactsApi(
                    new System.Uri("http://localhost:5136"));

            DataContext = new ContactsVM(contactsApi);

            this.Loaded += ContactsWindow_Loaded;
        }

        private async void ContactsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var window = (Window)sender;
            var contactsVM = (ContactsVM)window.DataContext;
            contactsVM.ContactsList = await contactsApi.GetContacts();
        }
    }
}
