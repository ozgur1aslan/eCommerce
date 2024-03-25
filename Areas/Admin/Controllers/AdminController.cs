using System.Security.Claims;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        private readonly eCommerceContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ISeasonRepository _seasonRepository;
        private IBrandRepository _brandRepository;
        private ITagRepository _tagRepository;

        
        

        public AdminController(eCommerceContext context, IProductRepository productRepository, ICategoryRepository categoryRepository, ISeasonRepository seasonRepository, IBrandRepository brandRepository, ITagRepository tagRepository)
        {
            _context = context;

            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _seasonRepository = seasonRepository;
            _brandRepository = brandRepository;
            _tagRepository = tagRepository;

            
        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Store()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }


        public IActionResult Orders()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            var orders = _context.Orders
            .Include(v => v.User)
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Product)
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Pictures)
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Values)
            .ToList();




            return View(orders);
        }


        public IActionResult OrderDetails(int? id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


            var order = _context.Orders
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Product)
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Pictures)
            .Include(v => v.PurchasedItems)
                .ThenInclude(i=>i.Variant)
                    .ThenInclude(i=>i.Values)
            .FirstOrDefault(item => item.OrderId == id);




            return View(order);
        }



        [HttpPost]
        public IActionResult ChangeOrderStatus(string newStatus, int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order != null)
            {
                order.OrderStatus = newStatus;
                _context.SaveChanges();
            }

            return RedirectToAction("OrderDetails", new { id = orderId });
        }


        public IActionResult Statistics()
        {
            return View();
        }


        public IActionResult MostPurchased()
        {
            var products = _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Brand)
                .Include(c => c.Gender)
                .Include(c => c.Variants)
                    .ThenInclude(v => v.PurchasedItems)
                .Include(c => c.Variants)
                    .ThenInclude(v => v.Pictures)
                .ToList();

            // Sort products based on total quantity of purchased items across all variants
            products = products.OrderByDescending(p => p.Variants.Sum(v => v.PurchasedItems.Sum(pi => pi.Quantity))).ToList();


            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
            ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();

            return View(products);
        }

        public IActionResult MostWishlisted()
        {
            var products = _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Brand)
                .Include(c => c.Gender)
                .Include(c => c.Variants)
                    .ThenInclude(v => v.WishlistItems)
                .Include(c => c.Variants)
                    .ThenInclude(v => v.Pictures)
                .ToList();

            // Sort products based on total quantity of purchased items across all variants
            products = products.OrderByDescending(p => p.Variants.Sum(v => v.WishlistItems.Count)).ToList();


            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
            ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();

            return View(products);
        }


        


    }
}