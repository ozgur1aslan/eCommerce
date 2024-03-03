using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController:Controller{


        private readonly eCommerceContext _context;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ISeasonRepository _seasonRepository;
        private IBrandRepository _brandRepository;
        private ITagRepository _tagRepository;

        private readonly IWebHostEnvironment _webHostEnvironment;

        

        public ProductController(eCommerceContext context, IProductRepository productRepository, ICategoryRepository categoryRepository, ISeasonRepository seasonRepository, IBrandRepository brandRepository, ITagRepository tagRepository, IWebHostEnvironment webHostEnvironment){

            
            _context = context;

            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _seasonRepository = seasonRepository;
            _brandRepository = brandRepository;
            _tagRepository = tagRepository;

            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<IActionResult> CreateAsync()
        {
            
            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Seasons = new SelectList(await _seasonRepository.Seasons.ToListAsync(), "SeasonId", "SeasonName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
            ViewBag.Genders = new SelectList(await _context.Genders.ToListAsync(), "GenderId", "GenderName");
            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;

            //var options = _context.Options.Include(p => p.Values).ToList();
            //ViewBag.Options = new SelectList(options, "OptionId", "OptionName");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductCreateViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    


                    // Map ViewModel to Product2 entity
                    var product = new Product
                    {
                        ProductName = model.ProductName,
                        Rating = model.Rating,
                        GenderId = model.GenderId,
                        CategoryId = model.CategoryId,
                        SeasonId = model.SeasonId,
                        BrandId = model.BrandId,
                        Description = model.Description
                    };

                    // Map ViewModel to ProductVariant entities
                    foreach (var variantModel in model.Variants)
                    {

                        if(variantModel.Price <= variantModel.DiscountedPrice ) {
                            TempData["Message"] = "Discounted price must be lower than the regular price.";

                            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
                            ViewBag.Seasons = new SelectList(await _seasonRepository.Seasons.ToListAsync(), "SeasonId", "SeasonName");
                            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
                            ViewBag.Genders = new SelectList(await _context.Genders.ToListAsync(), "GenderId", "GenderName");

                            return View(model);
                        };

                        var variant = new Variant
                        {
                            Price = variantModel.Price,
                            DiscountedPrice = variantModel.DiscountedPrice,
                            Stock = variantModel.Stock,
                            
                        };


                        foreach (var value in variantModel.Values)
                        {
                            Value x = _context.Values.FirstOrDefault(val => val.ValueId == value.ValueId);
                            variant.Values.Add(x);
                        }


                        // Process and save each uploaded image for the variant
                        foreach (var pictureFile in variantModel.Pictures)
                        {
                            var imageUrl = _productRepository.ProcessAndSaveImage(pictureFile);
                            variant.Pictures.Add(new Picture { Path = imageUrl });
                        }

                        

                        product.Variants.Add(variant);
                    }

                    // Save the product to the database
                    _productRepository.CreateProduct(product);

                    return RedirectToAction("List");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while processing the request.");
                // You may want to log the exception for further investigation
                // Logging.Log(ex);
            }

            // Provide necessary data for dropdowns
            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Seasons = new SelectList(await _seasonRepository.Seasons.ToListAsync(), "SeasonId", "SeasonName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
            ViewBag.Genders = new SelectList(await _context.Genders.ToListAsync(), "GenderId", "GenderName");

            return View(model);
        }
        




        public async Task<IActionResult> List()
        {

            var products = _productRepository.Products
            .Include(c => c.Category)
            .Include(c => c.Season)
            .Include(c => c.Brand)
            .Include(c => c.Gender)
            .Include(c => c.Variants)
            .ThenInclude(c => c.Pictures);

            
            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
            ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();

            return View(await products.ToListAsync());


        }  

        
        public IActionResult FilterProducts(int? categoryId, int? seasonId, int? brandId, int? genderId)
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
                    (!genderId.HasValue || p.GenderId == genderId))
                .ToList();


            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
            ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();

            return View("List", filteredProducts);
        }


        public IActionResult FilterProductsCheckbox(List<int?> categoryIds, List<int?> seasonIds, List<int?> brandIds)
        {
            var filteredProducts = _productRepository.Products
                .Where(p =>
                    (categoryIds == null || !categoryIds.Any() || categoryIds.Contains(p.CategoryId)) &&
                    (seasonIds == null || !seasonIds.Any() || seasonIds.Contains(p.SeasonId)) &&
                    (brandIds == null || !brandIds.Any() || brandIds.Contains(p.BrandId)))
                .ToList();

            ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
            ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
            ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();

            return View("List", filteredProducts);
        }



        


        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var product = _productRepository.Products
            .Include(i=>i.Tags)
            .Include(c => c.Category)
            .Include(c => c.Season)
            .Include(c => c.Brand)
            .Include(c => c.Gender)
            .FirstOrDefault(c=> c.ProductId == id);
            if(product == null){
                return NotFound();
            }

            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Seasons = new SelectList(await _seasonRepository.Seasons.ToListAsync(), "SeasonId", "SeasonName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
            ViewBag.Genders = new SelectList(await _context.Genders.ToListAsync(), "GenderId", "GenderName");
            ViewBag.Tags = _tagRepository.Tags.ToList();

            

            var productEditVM = new ProductEditViewModel();

                productEditVM.ProductId = product.ProductId;
                productEditVM.ProductName = product.ProductName;
                productEditVM.Tags = product.Tags;
                productEditVM.isActive = product.isActive;



                if(product.Description != null)
                {
                    productEditVM.Description = product.Description;
                }

                if(product.CategoryId != null)
                {
                    productEditVM.CategoryId = product.CategoryId;
                }

                if(product.SeasonId != null)
                {
                    productEditVM.SeasonId = product.SeasonId;
                }

                if(product.BrandId != null)
                {
                    productEditVM.BrandId = product.BrandId;
                }

                if(product.GenderId != null)
                {
                    productEditVM.GenderId = product.GenderId;
                }


            return View(productEditVM);
        }




        [HttpPost]
        public async Task<IActionResult> Edit(ProductEditViewModel productEditVM, int[] tagIds)
        {
            if(ModelState.IsValid){
                
                var entityToUpdate = new Product();

                entityToUpdate.ProductId = productEditVM.ProductId;
                entityToUpdate.ProductName = productEditVM.ProductName;
                productEditVM.isActive = productEditVM.isActive;

                bool x = productEditVM.isActive;

                if (productEditVM.Description != null)
                {
                    entityToUpdate.Description = productEditVM.Description;
                }

                if(productEditVM.CategoryId != null)
                {
                    entityToUpdate.CategoryId = productEditVM.CategoryId;
                }

                if(productEditVM.SeasonId != null)
                {
                    entityToUpdate.SeasonId = productEditVM.SeasonId;
                }

                if(productEditVM.BrandId != null)
                {
                    entityToUpdate.BrandId = productEditVM.BrandId;
                }

                if(productEditVM.GenderId != null)
                {
                    entityToUpdate.GenderId = productEditVM.GenderId;
                }



                _productRepository.EditProduct(entityToUpdate, tagIds, x);
                return RedirectToAction("List");
            }



            ViewBag.Categories = new SelectList(await _categoryRepository.Categories.ToListAsync(), "CategoryId", "CategoryName");
            ViewBag.Seasons = new SelectList(await _seasonRepository.Seasons.ToListAsync(), "SeasonId", "SeasonName");
            ViewBag.Brands = new SelectList(await _brandRepository.Brands.ToListAsync(), "BrandId", "BrandName");
            ViewBag.Brands = new SelectList(await _context.Brands.ToListAsync(), "GenderId", "GenderName");
            return View(productEditVM);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id){

            try
            {
                var productToDelete = await _productRepository.Products.FirstOrDefaultAsync(c=> c.ProductId == id);
            
                if(productToDelete == null){
                    return NotFound();
                }

                _productRepository.DeleteProduct(productToDelete);
                
                return RedirectToAction("List");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "You can't delete this product beacuse it has variants. Try deactivating it in the edit option.");

                var products = await _productRepository.Products
                .Include(c => c.Category)
                .Include(c => c.Season)
                .Include(c => c.Brand)
                .Include(c => c.Gender)
                .Include(c => c.Variants)
                .ThenInclude(c => c.Pictures).ToListAsync();

                ViewBag.Categories = _categoryRepository.Categories.Include(c => c.Products).ToList();
                ViewBag.Seasons = _seasonRepository.Seasons.Include(c => c.Products).ToList();
                ViewBag.Brands = _brandRepository.Brands.Include(c => c.Products).ToList();
                ViewBag.Genders = _context.Genders.Include(c => c.Products).ToList();


                return View("List", products);
            }

        }



        [HttpPost]
        public IActionResult ApplyDiscount(int discountedPrice)
        {

            return View();
        }


    }
}