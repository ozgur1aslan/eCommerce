using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using eCommerce.Data.Abstract;
using eCommerce.Models;
using eCommerce.Data.Concrete.EfCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        private readonly eCommerceContext _context;

        public CartService(eCommerceContext context, ICartRepository cartRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _cartRepository = cartRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _context = context;
            
        }

        public int GetCartItemCount()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _cartRepository.GetCartItemCount(userId);
        }


        public void AddToCart(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            if (userId != null)
            {
                // Check if the product is already in the wishlist
                if (!_cartRepository.CartItems.Any(item => item.UserId == userId && item.VariantId == variantId))
                {
                    var cartItem = new CartItem
                    {
                        UserId = userId,
                        VariantId = variantId
                    };

                    _cartRepository.AddToCart(cartItem);
                }
            }
        }

        public void RemoveFromCart(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _cartRepository.RemoveFromCart(variantId, userId);
        }
        

        public bool IsInCart(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _cartRepository.CartItems.Any(item => item.UserId == userId && item.VariantId == variantId);
        }




        public CartItem UpdateCartItemQuantity(int variantId, string userId, int change)
        {
            var cartItem = _cartRepository.CartItems.Include(v => v.Variant).FirstOrDefault(item => item.UserId == userId && item.VariantId == variantId);

            if (cartItem != null)
            {
                // Update the quantity
                cartItem.Quantity += change;

                // Ensure the quantity stays within the valid range (0 to max stock)
                cartItem.Quantity = Math.Max(1, Math.Min(cartItem.Variant.Stock, cartItem.Quantity));

                _context.SaveChanges();
            }

            return cartItem;
        }


    }
}