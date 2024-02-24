using System.ComponentModel.DataAnnotations;

namespace eCommerce.ViewModels
{
    public class EditViewModel{

        public string? Id {get;set;}
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; } = string.Empty;    

        public IList<string>? SelectedRoles {get;set;}   
    }
}