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


    }
}