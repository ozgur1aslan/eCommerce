using eCommerce.Data.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.ViewComponents
{
    public class  HighlightedProducts: ViewComponent
    {
        private IProductRepository _productRepository;
        public HighlightedProducts(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _productRepository
                                .Products
                                .Take(5)
                                .ToListAsync()
            );
        }
    }
}
