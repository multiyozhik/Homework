using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClientApp.Models;
using WpfClientApp.Services;

namespace WpfClientApp.ViewModels
{
    public class RegisterVM
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }

    }
}
