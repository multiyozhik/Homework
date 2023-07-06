using System.ComponentModel;

namespace _17_EFShop.Models
{
	class Shopping: IDataErrorInfo
    {
		public int Id { get; set; }
        public string? Email { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }

		private string? errorMessage;
        public string Error => errorMessage;

		public string this[string columnName]
        {
            get 
            {
                var error = string.Empty;
				switch (columnName)
                {
                    case "ProductCode":
                        if (ProductCode is null)
                            errorMessage = "Пустое поле";
						break;
					case "ProductName":
						if (ProductCode is null)
							errorMessage = "Пустое поле";
                        break;
				}
				errorMessage = error;
				return error;
			}
        }
	}
}
