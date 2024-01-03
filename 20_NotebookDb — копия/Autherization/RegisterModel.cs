using System.ComponentModel.DataAnnotations;

namespace _21_AuthNotebookDb.Autherization
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Login is required"), MaxLength(20)]
        public string LoginProp { get; set; }

        [Required(ErrorMessage = "Password is required"), DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
