using System.ComponentModel.DataAnnotations;

namespace _21_AuthNotebookDb.Autherization
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string LoginProp { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }

    }
}
