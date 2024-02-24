using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers;

public class HomeController : Controller
{
    private IProductRepository _productRepository;


        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

    public IActionResult Index()
    {
        //var products = _productRepository.Products.Include(c => c.Variants).ThenInclude(c => c.Pictures);


        var products = _productRepository.Products
        .Include(p => p.Variants)
        .ThenInclude(v => v.Pictures)
        .Where(p => p.Variants.Any(v => v.Stock > 0)) // Filter out products with no stock
        .ToList();

        // Select the product variants with the most stock and smallest variantId
        var selectedVariants = products.Select(p => p.Variants
            .Where(v => v.Stock > 0)
            .OrderByDescending(v => v.Stock)
            .ThenBy(v => v.VariantId)
            .FirstOrDefault())
            .Where(v => v != null)
            .ToList();

        //if(!string.IsNullOrEmpty(tag))
        //{
        //    posts = posts.Where(x => x.Tags.Any(t => t.Url == tag));
        //}

        //return View( new ProductViewModel { Products = await products.ToListAsync() });
        return View(selectedVariants);
    }





    public IActionResult Product(int productId)
    {
        var product = _productRepository.Products
            .Include(p => p.Category)
            .Include(p => p.Season)
            .Include(p => p.Brand)
            .Include(p => p.Tags)
            .FirstOrDefault(p => p.ProductId == productId);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }







    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}
