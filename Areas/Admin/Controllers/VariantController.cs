using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using eCommerce.ViewModels;
using eCommerce.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class VariantController:Controller{


        private readonly eCommerceContext _context;
        private IVariantRepository _variantRepository;
        private IProductRepository _productRepository;
        

        public VariantController(eCommerceContext context, IVariantRepository variantRepository, IProductRepository productRepository){


            _context = context;

            _variantRepository = variantRepository;

            _productRepository = productRepository;

        }



        public async Task<IActionResult> List(int? id)
        {

            var variants = _variantRepository.Variants
            .Include(v => v.Product)
                .ThenInclude(p => p.Gender)
            .Include(v => v.Product)
                .ThenInclude(p => p.Season)
            .Include(v => v.Product)
                .ThenInclude(p => p.Brand)
            .Include(v => v.Product)
                .ThenInclude(p => p.Category)
            .Include(v => v.Pictures)
            .Include(v => v.Values)
                .ThenInclude(v => v.Option)
            .Where(v => v.ProductId == id);

            

            return View(await variants.ToListAsync());
        }




        public async Task<IActionResult> CreateAsync(int? id)
        {
            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;

            ViewBag.Id = id;

            return View();
        }   



        [HttpPost]
        public IActionResult Create(NewVariantCreateViewModel variantModel)
        {
            try
            {
                if (ModelState.IsValid)
                    { 
                    
                        var variantToCreate = new Variant
                        {
                            Price = variantModel.Price,
                            DiscountedPrice = variantModel.DiscountedPrice,
                            Stock = variantModel.Stock,
                            ProductId = variantModel.ProductId
                        };


                        foreach (var value in variantModel.Values)
                        {
                            Value x = _context.Values.FirstOrDefault(val => val.ValueId == value.ValueId);
                            variantToCreate.Values.Add(x);
                        }


                        // Process and save each uploaded image for the variant
                        foreach (var pictureFile in variantModel.Pictures)
                        {
                            var imageUrl = _variantRepository.ProcessAndSaveImage(pictureFile);
                            variantToCreate.Pictures.Add(new Picture { Path = imageUrl });
                        }


                        _variantRepository.CreateVariant(variantToCreate);

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


            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;

            ViewBag.Id = variantModel.ProductId;
            return View(variantModel);
        }
            
        

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var variant = await _variantRepository.Variants
            .Include(v => v.Product)
                .ThenInclude(p => p.Gender)
            .Include(v => v.Product)
                .ThenInclude(p => p.Season)
            .Include(v => v.Product)
                .ThenInclude(p => p.Brand)
            .Include(v => v.Product)
                .ThenInclude(p => p.Category)
            .Include(v => v.Pictures)
            .Include(v => v.Values)
                .ThenInclude(v => v.Option)
            .Where(v => v.VariantId == id).FirstOrDefaultAsync();


            if (variant == null)
            {
                return NotFound();
            }

            var variantToEditVM = new EditVariantViewModel
                        {
                            VariantId = variant.VariantId,
                            Price = variant.Price,
                            DiscountedPrice = variant.DiscountedPrice,
                            Stock = variant.Stock,
                            ProductId = variant.ProductId,
                            Values = variant.Values
                            
                        };
                        


            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;

            return View(variantToEditVM);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditVariantViewModel variantVM, int[]? SelectedValues)
        {
            if (id != variantVM.VariantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                _variantRepository.UpdateVariant(variantVM, SelectedValues);

                return RedirectToAction("Index", "Home");
            }

            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;
            return View(variantVM);
        }



    
        
        

        [HttpPost]
        public async Task<IActionResult> Delete(int id){
            var variantToDelete = await _variantRepository.Variants.FirstOrDefaultAsync(c=> c.VariantId == id);
            
            if(variantToDelete == null){
                return NotFound();
            }

            var productId = variantToDelete.ProductId;


            

            try
            {
                _variantRepository.DeleteVariant(variantToDelete);


                var variants = await _variantRepository.Variants
                .Include(v => v.Product)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Season)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Brand)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Category)
                .Include(v => v.Pictures)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Option)
                .Where(v => v.ProductId == productId).ToListAsync();

                return View("List", variants);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "You can't delete this variant beacuse it's being used in at least one customer's wishlist/cart/order. Try deactivating it in the edit option.");



                var variants = await _variantRepository.Variants
                .Include(v => v.Product)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Season)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Brand)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Category)
                .Include(v => v.Pictures)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Option)
                .Where(v => v.ProductId == productId).ToListAsync();

                return View("List", variants);
            }

            
        }


    }
}