using eCommerce.Data.Abstract;
using eCommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController:Controller{


        private ITagRepository _tagRepository;


        public TagController(ITagRepository tagRepository){

            _tagRepository = tagRepository;


        }




        public async Task<IActionResult> List()
        {

            var tags = _tagRepository.Tags.Include(c => c.Products);


            return View(await tags.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if(ModelState.IsValid)
            {

                _tagRepository.CreateTag(tag);
                return RedirectToAction("List");
            }
            
            return View(tag);
        }


        public IActionResult Edit(int? id)
        {
            if(id == null){
                return NotFound();
            }

            var tag = _tagRepository.Tags.FirstOrDefault(c=> c.TagId == id);
            if(tag == null){
                return NotFound();
            }


            return View(tag);
        }


        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            if(ModelState.IsValid){
                
                _tagRepository.UpdateTag(tag);
                return RedirectToAction("List");
            }

            return View(tag);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id){

            var tagToDelete = await _tagRepository.Tags.FirstOrDefaultAsync(c=> c.TagId == id);
            
            if(tagToDelete == null){
                return NotFound();
            }

            _tagRepository.DeleteTag(tagToDelete);
            
            return RedirectToAction("List");
        }


    }
}