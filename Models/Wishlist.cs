namespace eCommerce.Models
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public string UserId { get; set; }
        public int? VariantId { get; set; }

        // Navigation properties
        public AppUser User { get; set; }
        public Variant? Variant { get; set; }
    }
}