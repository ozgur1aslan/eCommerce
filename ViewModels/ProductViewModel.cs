using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> Products { get; set; } = new();
    }
}