using System.Linq;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfCartRepository : ICartRepository
    {
        private readonly eCommerceContext _context;

        public EfCartRepository(eCommerceContext context)
        {
            _context = context;
        }


        public IQueryable<CartItem> CartItems => _context.CartItems;

        public int GetCartItemCount(string userId)
        {
            return _context.CartItems.Count(item => item.UserId == userId);
        }

        public void AddToCart(CartItem cartItem)
        {
            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }

        public void RemoveFromCart(int variantId, string userId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(item => item.UserId == userId && item.VariantId == variantId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
        }


        public void ToggleCartItem(int variantId, string userId)
        {
            // Your logic to toggle the wishlist item (add if not exists, remove if exists)
            // Example logic: Check if the item exists, if yes, remove it; if not, add it.
            var existingItem = _context.CartItems.FirstOrDefault(wi => wi.VariantId == variantId && wi.UserId == userId);

            if (existingItem != null)
            {
                _context.CartItems.Remove(existingItem);

            }
            else
            {
                var newItem = new CartItem
                {
                    UserId = userId,
                    VariantId = variantId,
                    Quantity = 1
                };
                _context.CartItems.Add(newItem);
            }

            _context.SaveChanges();
        }
        
        
    }
}