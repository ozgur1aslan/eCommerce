using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class OrderCreateViewModel{

        [Required]
        public string UserId {get;set;}

        
        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]

        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Fake card number must be a 16-digit number.")]
        public string? CardNumber { get; set; }
    }

    public class OrderCardItemsViewModel
    {
        public string VariantId { get; set; }
        public string Quantity { get; set; }
    }
}