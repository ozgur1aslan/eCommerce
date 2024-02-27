using eCommerce.Data.Abstract;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController:Controller{


        private ICategoryRepository _categoryRepository;


        public CategoryController(ICategoryRepository categoryRepository){

            _categoryRepository = categoryRepository;


        }




        public async Task<IActionResult> List()
        {

            var categories = _categoryRepository.Categories.Include(c => c.Products);


            return View(await categories.ToListAsync());
        } 

        public IActionResult Create()
        {
            return View();
        }   

        [HttpPost]
        public IActionResult Create(Category ctg)
        {
            if(ModelState.IsValid)
            {

                _categoryRepository.CreateCategory(ctg);
                return RedirectToAction("List");
            }
            
            return View(ctg);
        }


        public IActionResult Edit(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var category = _categoryRepository.Categories.FirstOrDefault(c=> c.CategoryId == id);
            if(category == null){
                return NotFound();
            }


            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category ctg)
        {
            if(ModelState.IsValid){
                
                _categoryRepository.EditCategory(ctg);
                return RedirectToAction("List");
            }

            return View(ctg);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id){

            var categoryToDelete = await _categoryRepository.Categories.FirstOrDefaultAsync(c=> c.CategoryId == id);
            
            if(categoryToDelete == null){
                return NotFound();
            }

            _categoryRepository.DeleteCategory(categoryToDelete);
            
            return RedirectToAction("List");
        }


    }
}


