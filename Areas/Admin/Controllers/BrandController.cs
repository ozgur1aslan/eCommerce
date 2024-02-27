using eCommerce.Data.Abstract;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController:Controller{


        private IBrandRepository _brandRepository;


        public BrandController(IBrandRepository brandRepository){

            _brandRepository = brandRepository;


        }



        public async Task<IActionResult> List()
        {

            var brands = _brandRepository.Brands.Include(c => c.Products);


            


            return View(await brands.ToListAsync());
        } 

        public IActionResult Create()
        {
            return View();
        }   

        [HttpPost]
        public IActionResult Create(Brand brd)
        {
            if(ModelState.IsValid)
            {

                _brandRepository.CreateBrand(brd);
                return RedirectToAction("List");
            }
            
            return View(brd);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var brand = _brandRepository.Brands.FirstOrDefault(c=> c.BrandId == id);
            if(brand == null){
                return NotFound();
            }


            return View(brand);
        }


        [HttpPost]
        public IActionResult Edit(Brand brd)
        {
            if(ModelState.IsValid){
                
                _brandRepository.UpdateBrand(brd);
                return RedirectToAction("List");
            }

            return View(brd);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id){

            var brandToDelete = await _brandRepository.Brands.FirstOrDefaultAsync(c=> c.BrandId == id);
            
            if(brandToDelete == null){
                return NotFound();
            }

            _brandRepository.DeleteBrand(brandToDelete);
            
            return RedirectToAction("List");
        }


    }
}