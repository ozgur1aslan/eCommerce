using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Order{
        public int OrderId { get; set; }

        [Required]
        public string UserId {get;set;}
        public AppUser User {get;set;}

        [Required]
        public List<PurchasedItem> PurchasedItems {get;set;} = new List<PurchasedItem>();

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public DateTime PurchaseDate {get;set;}

        [Required]
        public string OrderStatus {get;set;}

        [Required]
        public string Address { get; set; }

        [Required]

        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Fake card number must be a 16-digit number.")]
        public string? CardNumber { get; set; }
    }


    public class PurchasedItem
    {
        public int PurchasedItemId { get; set; }
        public int OrderId { get; set; }
        public int? VariantId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public Order Order { get; set; }
        public Variant? Variant { get; set; }
    }
}
