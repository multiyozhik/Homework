using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfClientApp.Models
{
    public class Contact : IDataErrorInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }

        private string? errorMessage;
        public string Error
        {
            get => errorMessage;
        }

        public string this[string columnName]
        {
            get
            {
                var error = string.Empty;
                switch (columnName)
                {
                    case "FamilyName":
                        if (!StringIsCorrect(FamilyName))
                            error = "Ошибка ввода фамилии. Фамилия должна содержать не менее 2 букв";
                        break;
                    case "Name":
                        if (!StringIsCorrect(Name))
                            error = "Ошибка ввода имени. Имя должно содержать не менее 2 букв";
                        break;
                    case "MiddleName":
                        if (!StringIsCorrect(MiddleName))
                            error = "Ошибка ввода отчества. Отчество должно содержать не менее 2 букв";
                        break;
                    case "PhoneNumber":
                        //var pattern = new Regex("8-[0-9]{3}-[0-9]{3}-[0-9]{4}");
                        //var pattern = new Regex("@+7/@([0-9]{3}@)[0-9]{3}-[0-9]{4}");
                        //var pattern = new Regex("8-[0-9]{3}-[0-9]{3}-[0-9]{4}");
                        var pattern = new Regex(@"\+7\(\d{3}\)\d{3}-\d{4}");

                        if (PhoneNumber is null || !Regex.IsMatch(PhoneNumber, pattern.ToString()))
                            error = "Ошибка ввода номера телефона. Необходимо ввести в формате +7(ххх)ххх-хххх";
                        break;
                    case "Adress":
                        if (!StringAdressIsCorrect(Adress))
                            error = "Ошибка ввода адреса. Адрес должен содержать не менее 10 букв";
                        break;
                }
                errorMessage = error;
                return error;
            }
        }

        private bool StringIsCorrect(string str) =>
            str is not null && str.Length >= 2 && str.All(char.IsLetter);

        private bool StringAdressIsCorrect(string str) =>
            str is not null && str.Length >= 10;
    }
}
