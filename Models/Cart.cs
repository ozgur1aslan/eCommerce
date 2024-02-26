
namespace eCommerce.Models
{
    

    public class CartItem
    {
        public int CartItemId { get; set; }
        public string UserId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }

        // Navigation properties
        public AppUser User { get; set; }
        public Variant Variant { get; set; }
    }
}








                    