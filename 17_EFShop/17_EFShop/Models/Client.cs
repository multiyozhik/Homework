using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

namespace _17_EFShop
{
	public class Client : IDataErrorInfo
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string FamilyName { get; set; }
		public string MiddleName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }

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
						case "FirstName":
							if (!StringIsCorrect(FirstName))
								error = "Ошибка ввода имени. Имя должно содержать не менее 2 букв";
							break;
						case "MiddleName":
							if (!StringIsCorrect(MiddleName))
								error = "Ошибка ввода отчества. Отчество должно содержать не менее 2 букв";
							break;
						case "PhoneNumber":
							var pattern = new Regex("8-[0-9]{3}-[0-9]{3}-[0-9]{4}");

							if (PhoneNumber is null || !Regex.IsMatch(PhoneNumber, pattern.ToString()))
								error = "Ошибка ввода номера телефона. Необходимо ввести в формате 8-ххх-ххх-хххх";
							break;
						case "Email":
							try
							{
								var mailAddress = new System.Net.Mail.MailAddress(Email);
							}
							catch (Exception ex)
							{
								error = $"Ошибка ввода адреса электронной почты: {ex.Message}";
							}
							break;
					}
				errorMessage = error;
				return error;
			}
		}

		private bool StringIsCorrect(string str) => 
			str is not null && str.Length >= 2 && str.All(char.IsLetter);		
	}
}
