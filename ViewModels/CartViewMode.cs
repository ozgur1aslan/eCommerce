using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class CartViewModel
    {
        
        public decimal TotalPrice { get; set; }

        public List<CartItem>? CartItems { get; set; } = new List<CartItem>();

        
    }
}