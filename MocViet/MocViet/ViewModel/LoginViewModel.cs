using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MocViet.ViewModel
{
    public class LoginViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }

}
