using Microsoft.EntityFrameworkCore;
using System.Windows;
using WpfClientApp.Services;
using WpfClientApp.ViewModels;

namespace WpfClientApp.Views
{
    /// <summary>
    /// Логика взаимодействия для Contacts.xaml
    /// </summary>
    public partial class ContactsWindow : Window
    {
        public ContactsWindow()
        {
            InitializeComponent();            
            DataContext = new ContactsVM(
                new ContactsApi(
                    new System.Uri("http://localhost:5136")));
        }
    }
}
