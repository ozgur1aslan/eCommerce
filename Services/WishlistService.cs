using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using eCommerce.Data.Abstract;
using eCommerce.Models;

namespace eCommerce.Services
{
    public class WishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public WishlistService(IWishlistRepository wishlistRepository, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _wishlistRepository = wishlistRepository;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            
        }

        public int GetWishlistItemCount()
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _wishlistRepository.GetWishlistItemCount(userId);
        }

        public void AddToWishlist(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);

            if (userId != null)
            {
                // Check if the product is already in the wishlist
                if (!_wishlistRepository.WishlistItems.Any(item => item.UserId == userId && item.VariantId == variantId))
                {
                    var wishlistItem = new WishlistItem
                    {
                        UserId = userId,
                        VariantId = variantId
                    };

                    _wishlistRepository.AddToWishlist(wishlistItem);
                }
            }
        }

        public void RemoveFromWishlist(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            _wishlistRepository.RemoveFromWishlist(variantId, userId);
        }
        

        public bool IsInWishlist(int variantId)
        {
            var userId = _userManager.GetUserId(_httpContextAccessor.HttpContext.User);
            return _wishlistRepository.WishlistItems.Any(item => item.UserId == userId && item.VariantId == variantId);
        }



    }
}