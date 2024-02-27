using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController:Controller{
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(RoleManager<AppRole> roleManager){
             _roleManager = roleManager;
        }
        public IActionResult Index(){
            return View(_roleManager.Roles);
        }

        public IActionResult Create(){
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AppRole model){
            var result = await _roleManager.CreateAsync(model);
            if(result.Succeeded){
                return RedirectToAction("Index");
            }

            foreach(var err in result.Errors){
                ModelState.AddModelError("",err.Description);
            }

            return View(model);
        }



        public async Task<IActionResult> Edit(string id)
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }

                var role = await _roleManager.FindByIdAsync(id);

                if (role == null)
                {
                    return RedirectToAction("Index");
                }

                return View(role);
            }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, AppRole model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(id);

                if (role != null)
                {
                    role.Name = model.Name;

                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }

            // If the ModelState is not valid or the update fails, return to the view with the model
            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                // Handle errors as needed
            }

            return RedirectToAction("Index");
        }


    }
}