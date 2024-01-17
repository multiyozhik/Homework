using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClientApp.Views;

namespace WpfClientApp.ViewModels
{
    public class ApplicationVM
    {
        private readonly RelayCommand? loadContactsDbCommand;
        public RelayCommand LoadContactsDbCommand
        {
            //Оператор null-объединения ?? возвращает левый операнд, если этот операнд не равен null, иначе-правый операнд
            get => loadContactsDbCommand ?? new RelayCommand(
                obj =>
                {
                    using (var dbContext = new ContactsDbContext())
                    {
                        var contactsWindow = new ContactsWindow()
                        {
                            DataContext = new ContactsVM(dbContext)                            
                        };
                        contactsWindow.ShowDialog();
                    }
                }
                );
        }
    }
}
