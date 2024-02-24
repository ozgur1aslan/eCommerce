using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}