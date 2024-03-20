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

    private ICategoryRepository _categoryRepository;
    private ISeasonRepository _seasonRepository;
    private IBrandRepository _brandRepository;
    private ICommentRepository _commentRepository;
    
    private readonly eCommerceContext _context;


        public HomeController(UserManager<AppUser> userManager, eCommerceContext context, IProductRepository productRepository, IVariantRepository variantRepository, ICategoryRepository categoryRepository, ISeasonRepository seasonRepository, IBrandRepository brandRepository, ICommentRepository commentRepository)
        {
            _productRepository = productRepository;
            _variantRepository = variantRepository;
            _categoryRepository = categoryRepository;
            _seasonRepository = seasonRepository;
            _brandRepository = brandRepository;
            _commentRepository = commentRepository;
            _context = context;

            _userManager = userManager;
        }

    public IActionResult Index(string gender, int? categoryId, int? seasonId, int? brandId, int? genderId)
    {
        //var products = _productRepository.Products.Include(c => c.Variants).ThenInclude(c => c.Pictures);


        var products = _productRepository.Products
        .Include(p => p.Gender)
        .Include(p => p.Comments)
        .Include(p => p.Variants)
        .ThenInclude(v => v.Pictures)
        .Where(p => p.Variants.Any(v => v.Stock > 0)) // Filter out products with no stock
        .ToList();


        if (!string.IsNullOrEmpty(gender))
        {
            products = products.Where(p => p.Gender != null && p.Gender.GenderName == gender).ToList();
            
        }


        // Select the product variants with the most stock and smallest variantId
        var selectedVariants = products
            .Where(v => v.isActive == true).Select(p => p.Variants
            .Where(v => v.Stock > 0)
            .OrderByDescending(v => v.Stock)
            .ThenBy(v => v.VariantId)
            .FirstOrDefault())
            .Where(v => v != null)
            .ToList();


        ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
        ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
        ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
        ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();

        return View(selectedVariants);
    }

    public IActionResult FilteredIndex0(int? categoryId, int? seasonId, int? brandId, int? genderId)
        {
            var filteredProducts = _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Brand)
                .Include(c => c.Gender)
                .Include(c => c.Variants)
                .ThenInclude(c => c.Pictures)
                .Where(p =>
                    (!categoryId.HasValue || p.CategoryId == categoryId) &&
                    (!seasonId.HasValue || p.SeasonId == seasonId) &&
                    (!brandId.HasValue || p.BrandId == brandId) &&
                    (!genderId.HasValue || p.GenderId == genderId) &&
                     p.Variants.Any(v => v.Stock > 0))
                .ToList();


            var selectedVariants = filteredProducts
                .Where(v => v.isActive == true).Select(p => p.Variants
                .Where(v => v.Stock > 0)
                .OrderByDescending(v => v.Stock)
                .ThenBy(v => v.VariantId)
                .FirstOrDefault())
                .Where(v => v != null)
                .ToList();

            return View(selectedVariants);
        }

        public IActionResult Filter(List<int?> categoryIds, List<int?> seasonIds, List<int?> brandIds, List<int?> genderIds)
        {
            var filteredProducts = _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Brand)
                .Include(c => c.Gender)
                .Include(c => c.Variants)
                    .ThenInclude(c => c.Pictures)
                .Where(p =>
                    (categoryIds == null || !categoryIds.Any() || categoryIds.Contains(p.CategoryId)) &&
                    (seasonIds == null || !seasonIds.Any() || seasonIds.Contains(p.SeasonId)) &&
                    (brandIds == null || !brandIds.Any() || brandIds.Contains(p.BrandId)) &&
                    (genderIds == null || !genderIds.Any() || genderIds.Contains(p.GenderId)))
                .ToList();

            var selectedVariants = filteredProducts
                .Where(v => v.isActive == true).Select(p => p.Variants
                .Where(v => v.Stock > 0)
                .OrderByDescending(v => v.Stock)
                .ThenBy(v => v.VariantId)
                .FirstOrDefault())
                .Where(v => v != null)
                .ToList();

            
            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
            ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();


            return View("Index", selectedVariants);
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

        var product = _context.Products
        .Include(p => p.Comments)
        .FirstOrDefault(p => p.ProductId == variant.Product.ProductId);

        if (product == null)
        {
            return NotFound();
        }


        double averageRating = 0;
        if (product.Comments.Count > 0)
        {
            averageRating = product.Comments.Average(c => c.Rating);
        }

        ViewBag.AverageRating = averageRating;
        ViewBag.CommentCount = product.Comments.Count;

        return View(variant);
    }



    [Authorize]
    [HttpPost]
    public async Task<JsonResult> AddCommentAsync(int ProductId, string Text, int Rating)
    {
        var userId = _userManager.GetUserId(User);

        // Check if the user has already commented on the specified product
        bool hasCommented = _context.Comments.Any(c => c.UserId == userId && c.ProductId == ProductId);

        if (hasCommented)
        {
            return Json(new { error = "You have already left a comment for this product." });
        }

        // Check if the user has purchased the product
        bool hasPurchased = HasUserPurchasedProduct(userId, ProductId);

        if (!hasPurchased)
        {
            // Handle the case where the user hasn't purchased the product
            return Json(new { error = "You need to purchase the product in order to make a comment." });
        }

        var user = await _userManager.GetUserAsync(User);
        var username = user.FullName;

        var entity = new Comment
        {
            ProductId = ProductId,
            Text = Text,
            Rating = Rating,
            PublishedOn = DateTime.Now,
            UserId = userId
        };

        _commentRepository.CreateComment(entity);

        return Json(new { 
            fullname = username,
            text = Text,
            rating = Rating,
            publishedOn = entity.PublishedOn
        });
    }


    public bool HasUserPurchasedProduct(string userId, int productId)
    {
        // Check if the user has purchased any order containing a variant linked to the specified product
        return _context.Orders
            .Any(order => order.UserId == userId &&
                        order.PurchasedItems.Any(item => item.Variant != null && item.Variant.ProductId == productId));
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
