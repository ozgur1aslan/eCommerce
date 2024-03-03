namespace eCommerce.Models;
public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedOn { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public string UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public int Rating { get; set; } // Add this property for rating
    }
