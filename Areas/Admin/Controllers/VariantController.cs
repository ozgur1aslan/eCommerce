using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using eCommerce.ViewModels;
using eCommerce.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
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
                            var imageUrl = _productRepository.ProcessAndSaveImage(pictureFile);
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
            
        

        public IActionResult Edit(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var variant = _variantRepository.Variants
            .Include(v => v.Product)
            .Include(v => v.Pictures)
            .Include(v => v.Values)
                .ThenInclude(v => v.Option)
            .FirstOrDefault(v => v.VariantId == id);

            if(variant == null){
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
        public IActionResult Edit(EditVariantViewModel variantToEditVM)
        {

            if(variantToEditVM == null){
                return NotFound();
            }

            
            if(ModelState.IsValid){
                
            var variant = new Variant
                        {
            VariantId = variantToEditVM.VariantId,
            Price = variantToEditVM.Price,
            DiscountedPrice = variantToEditVM.DiscountedPrice,
            Stock = variantToEditVM.Stock,
            ProductId = variantToEditVM.ProductId,
            Values = variantToEditVM.Values
                        };
            


            _variantRepository.UpdateVariant(variant);

            return RedirectToAction("List");
            }


            var options = _context.Options.Include(p => p.Values).ToList();
            ViewBag.Options = options;
            return View(variantToEditVM);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id){

            var variantToDelete = await _variantRepository.Variants.FirstOrDefaultAsync(c=> c.VariantId == id);
            
            if(variantToDelete == null){
                return NotFound();
            }

            _variantRepository.DeleteVariant(variantToDelete);
            
            return RedirectToAction("List");
        }


    }
}