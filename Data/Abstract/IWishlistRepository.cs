using System.Linq;
using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IWishlistRepository
    {
        IQueryable<WishlistItem> WishlistItems { get; }
        int GetWishlistItemCount(string userId);
        void AddToWishlist(WishlistItem wishlistItem);
        void RemoveFromWishlist(int productId, string userId);
        void ToggleWishlistItem(int productId, string userId);
    }
}