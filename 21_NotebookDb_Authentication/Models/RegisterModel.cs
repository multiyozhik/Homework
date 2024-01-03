using System.ComponentModel.DataAnnotations;

namespace _21_NotebookDb.Models
{
    public class RegisterModel
    {
        //[Required(ErrorMessage = "User Name is required")]
        //public string UserName { get; set; }

        //[EmailAddress]
        //[Required(ErrorMessage = "Email is required")]
        //public string Email { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        //public string Password { get; set; }



        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
