namespace eCommerce.Models
{
    public class WishlistItem
    {
        public int WishlistItemId { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation properties
        public AppUser User { get; set; }
        public Product Product { get; set; }
    }


}
