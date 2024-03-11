using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data.Concrete.EfCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private IProductRepository _productRepository;
    private IVariantRepository _variantRepository;
    private ICommentRepository _commentRepository;
    
    private readonly eCommerceContext _context;


        public HomeController(UserManager<AppUser> userManager, eCommerceContext context, IProductRepository productRepository, IVariantRepository variantRepository, ICommentRepository commentRepository)
        {
            _productRepository = productRepository;
            _variantRepository = variantRepository;
            _commentRepository = commentRepository;
            _context = context;

            _userManager = userManager;
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
        .ThenInclude(p => p.Tags)
    .Include(v => v.Product)
        .ThenInclude(p => p.Brand)
    .Include(v => v.Product)
        .ThenInclude(p => p.Season)
    .Include(v => v.Product)
        .ThenInclude(p => p.Category)
    .Include(v => v.Product)
        .ThenInclude(p => p.Gender)
    .Include(v => v.Product)
        .ThenInclude(p => p.Comments)
            .ThenInclude(c => c.User) // Include the User property within Comments
    .Include(v => v.Pictures)
    .Include(v => v.Values)
        .ThenInclude(v => v.Option)
    .FirstOrDefault(v => v.VariantId == variantId);


        var productIdToFilter = variant.Product.ProductId;
        var valueIdToFilter = variant.Values.FirstOrDefault(val => val.Option.OptionName == "Color")?.ValueId;


        var filteredVariants = _variantRepository.Variants
            .Include(v => v.Pictures)
            .Include(v => v.Values)
                .ThenInclude(val => val.Option)
            .Where(v => v.Product.ProductId == productIdToFilter && v.Values.Any(val => val.ValueId == valueIdToFilter))
            .ToList();


        var filteredVariants2 = _variantRepository.Variants
                .Include(v => v.Pictures)
                .Include(v => v.Values)
                    .ThenInclude(val => val.Option)
                .Where(v => v.Product.ProductId == productIdToFilter)
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



    [Authorize]
    [HttpPost]
    public async Task<JsonResult> AddCommentAsync(int ProductId, string Text, int Rating)
    {
        var userId = _userManager.GetUserId(User);

        var user = await _userManager.GetUserAsync(User);
        var username = user.FullName;

        var entity = new Comment {
            ProductId = ProductId,
            Text = Text,
            Rating = Rating,
            PublishedOn = DateTime.Now,
            UserId = userId
        };

        _commentRepository.CreateComment(entity);

        return Json(new { 
            username,
            Text,
            entity.PublishedOn,
            Rating
        });

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
