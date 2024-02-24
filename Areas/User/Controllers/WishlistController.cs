using System.Linq;
using eCommerce.Data.Abstract;
using eCommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.User.Controllers
{

    [Authorize]
    [Area("User")]
    public class WishlistController : Controller
    {
        private readonly IWishlistRepository _wishlistRepository;
        private readonly WishlistService _wishlistService;

        public WishlistController(IWishlistRepository wishlistRepository, WishlistService wishlistService)
        {
            _wishlistRepository = wishlistRepository;
            _wishlistService = wishlistService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var wishlistItems = _wishlistRepository.WishlistItems.Include(i=>i.Product).Where(item => item.UserId == userId).ToList();
            return View(wishlistItems);
        }

        [HttpGet]
        public JsonResult GetWishlistItemCount()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            
            var count = _wishlistRepository.GetWishlistItemCount(userId);
            return Json(count);
        }


        [HttpPost]
        public IActionResult AddToWishlist(int productId)
        {
            _wishlistService.AddToWishlist(productId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromWishlist(int productId)
        {
            _wishlistService.RemoveFromWishlist(productId);
            return RedirectToAction("Index", "Wishlist");
        }


        [HttpPost]
        public JsonResult ToggleWishlist(int productId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Toggle the wishlist item (remove if exists, add if not)
            _wishlistRepository.ToggleWishlistItem(productId, userId);

            // Get the updated count
            var count = _wishlistRepository.GetWishlistItemCount(userId);

            return Json(count);
        }

        
    }
}