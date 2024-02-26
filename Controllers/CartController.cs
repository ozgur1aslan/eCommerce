// Controllers/CartController.cs

using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Services;
using Microsoft.AspNetCore.Identity;
using eCommerce.Data.Abstract;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly CartService _cartService;

        public CartController(ICartRepository cartRepository, CartService cartService)
        {
            _cartRepository = cartRepository;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var cartItems = _cartRepository.CartItems
            .Include(i=>i.Variant)
                .ThenInclude(i=>i.Product)
            .Include(i=>i.Variant)
                .ThenInclude(i=>i.Pictures)
            .Include(i=>i.Variant)
                .ThenInclude(i=>i.Values)
            .Where(item => item.UserId == userId).ToList();
            return View(cartItems);
        }

        [HttpGet]
        public JsonResult GetCartItemCount()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            
            
            var count = _cartRepository.GetCartItemCount(userId);
            return Json(count);
        }


        [HttpPost]
        public IActionResult AddToCart(int variantId)
        {
            _cartService.AddToCart(variantId);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int variantId)
        {
            _cartService.RemoveFromCart(variantId);
            return RedirectToAction("Index", "Wishlist");
        }


        [HttpPost]
        public JsonResult ToggleCart(int variantId)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Toggle the wishlist item (remove if exists, add if not)
            _cartRepository.ToggleCartItem(variantId, userId);

            // Get the updated count
            var count = _cartRepository.GetCartItemCount(userId);

            return Json(count);
        }




        [HttpPost]
        public JsonResult UpdateCartItemQuantity(int variantId, int change)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // Update the quantity and get the updated cart item
            var updatedCartItem = _cartService.UpdateCartItemQuantity(variantId, userId, change);

            return Json(updatedCartItem.Quantity);
        }


        
    }
}
