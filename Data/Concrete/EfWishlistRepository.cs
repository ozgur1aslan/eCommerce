using System.Linq;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfWishlistRepository : IWishlistRepository
    {
        private readonly eCommerceContext _context;

        public EfWishlistRepository(eCommerceContext context)
        {
            _context = context;
        }

        public IQueryable<WishlistItem> WishlistItems => _context.WishlistItems;

        public int GetWishlistItemCount(string userId)
        {
            return _context.WishlistItems.Count(item => item.UserId == userId);
        }

        public void AddToWishlist(WishlistItem wishlistItem)
        {
            _context.WishlistItems.Add(wishlistItem);
            _context.SaveChanges();
        }

        public void RemoveFromWishlist(int productId, string userId)
        {
            var wishlistItem = _context.WishlistItems.FirstOrDefault(item => item.UserId == userId && item.ProductId == productId);

            if (wishlistItem != null)
            {
                _context.WishlistItems.Remove(wishlistItem);
                _context.SaveChanges();
            }
        }


        public void ToggleWishlistItem(int productId, string userId)
        {
            // Your logic to toggle the wishlist item (add if not exists, remove if exists)
            // Example logic: Check if the item exists, if yes, remove it; if not, add it.
            var existingItem = _context.WishlistItems.FirstOrDefault(wi => wi.ProductId == productId && wi.UserId == userId);

            if (existingItem != null)
            {
                _context.WishlistItems.Remove(existingItem);
            }
            else
            {
                var newItem = new WishlistItem
                {
                    UserId = userId,
                    ProductId = productId
                };
                _context.WishlistItems.Add(newItem);
            }

            _context.SaveChanges();
        }
    }
}