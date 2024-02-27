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
            
        

        // GET: Variant/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var variant = await _context.Variants.FindAsync(id);
        if (variant == null)
        {
            return NotFound();
        }

        var options = _context.Options.Include(p => p.Values).ToList();
    ViewBag.Options = options;

        return View(variant);
    }

    // POST: Variant/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("VariantId,Price,DiscountedPrice,Stock")] Variant variant)
    {
        if (id != variant.VariantId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(variant);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VariantExists(variant.VariantId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Index", "Home"); // Redirect to wherever you want after editing
        }

        var options = _context.Options.Include(p => p.Values).ToList();
    ViewBag.Options = options;
        return View(variant);
    }

    private bool VariantExists(int id)
    {
        return _context.Variants.Any(e => e.VariantId == id);
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