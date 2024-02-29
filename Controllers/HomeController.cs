using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data.Concrete.EfCore;

namespace eCommerce.Controllers;

public class HomeController : Controller
{
    private IProductRepository _productRepository;
    private IVariantRepository _variantRepository;
    
    private readonly eCommerceContext _context;


        public HomeController(eCommerceContext context, IProductRepository productRepository, IVariantRepository variantRepository)
        {
            _productRepository = productRepository;
            _variantRepository = variantRepository;
            _context = context;
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
        var selectedVariants = products.Where(v => v.isActive == true).Select(p => p.Variants
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





    public IActionResult Product(int variantId)
    {
        var variant = _variantRepository.Variants
            .Include(v => v.Product)
                .ThenInclude(v => v.Tags)
            .Include(v => v.Product)
                .ThenInclude(v => v.Brand)
            .Include(v => v.Product)
                .ThenInclude(v => v.Season)
            .Include(v => v.Product)
                .ThenInclude(v => v.Category)
            .Include(v => v.Product)
                .ThenInclude(v => v.Gender)
            .Include(v => v.Pictures)
            .Include(v => v.Values)
                .ThenInclude(v => v.Option)
            .FirstOrDefault(v => v.VariantId == variantId);

        var productId = variant.ProductId;

        var variantFamily = _variantRepository.Variants
        .Include(v => v.Pictures)
        .Include(v => v.Values)
            .ThenInclude(v => v.Option)
        .Where(v => v.ProductId == productId && v.VariantId != variant.VariantId  && v.Values.Any(val => val.OptionId == 2))
        .GroupBy(v => v.Values.FirstOrDefault(val => val.OptionId == 2).OptionId) // Group by color (OptionId 2)
        .Select(group => group.First()) // Select the first variant for each color
        .ToList();





var valueIdToFilter = variant.Values[1].ValueId;
var productIdToFilter = variant.Product.ProductId;

var filteredVariants = _variantRepository.Variants
    .Include(v => v.Pictures)
    .Include(v => v.Values)
        .ThenInclude(val => val.Option)
    .Where(v => v.Product.ProductId == productIdToFilter && v.Values.Any(val => val.ValueId == valueIdToFilter))
    .ToList();


var valueIdToFilter2 = variant.Values[1].ValueId;
var productIdToFilter2 = variant.Product.ProductId;




var filteredVariants2 = _variantRepository.Variants
        .Include(v => v.Pictures)
        .Include(v => v.Values)
            .ThenInclude(val => val.Option)
        .Where(v => v.Product.ProductId == variant.Product.ProductId)
        .AsEnumerable()  // Switch to client-side evaluation
        .Where(v => v.Values.Any(val => val.OptionId == 2 && val.ValueId != variant.Values
                        .FirstOrDefault(vv => vv.OptionId == 2)?.ValueId))
        .AsEnumerable()
    .GroupBy(v => v.Values.FirstOrDefault(val => val.OptionId == 2)?.ValueId)
    .Select(group => group.First())
    .ToList();







        ViewBag.VariantSizes = filteredVariants;

        
        ViewBag.VariantFamily = filteredVariants2;

        
        



        if (variant == null)
        {
            return NotFound();
        }

        return View(variant);
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
