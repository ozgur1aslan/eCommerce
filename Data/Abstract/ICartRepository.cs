using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface ICartRepository
    {
        IQueryable<CartItem> CartItems { get; }
        int GetCartItemCount(string userId);
        void AddToCart(CartItem CartItem);
        void RemoveFromCart(int? variantId, string userId);
        void ToggleCartItem(int variantId, string userId);
    }
}