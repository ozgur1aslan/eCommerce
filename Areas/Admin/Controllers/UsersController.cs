using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace eCommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController:Controller{

        private UserManager<AppUser> _userManager;
        private RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager){
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(){
            return View(_userManager.Users);
        }

        public IActionResult Create(){
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model){
            if(ModelState.IsValid){
                var user = new AppUser{UserName = model.Email, Email = model.Email, FullName = model.FullName};
                IdentityResult result =  await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded){
                    return RedirectToAction("Index");
                }

                foreach(IdentityError err in result.Errors){
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {

            if(id==null){
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(id);

            if(user != null){

                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();
                return View(new EditViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    SelectedRoles = await _userManager.GetRolesAsync(user)
                });
                }
                    
                return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditViewModel model)
        {
            if (id != model.Id)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    // Update user properties
                    user.Email = model.Email;
                    user.FullName = model.FullName;

                    // Get current roles for the user
                    var currentRoles = await _userManager.GetRolesAsync(user) ?? new List<string>();

                    // Ensure model.SelectedRoles is not null
                    var selectedRoles = model.SelectedRoles != null ? model.SelectedRoles : Enumerable.Empty<string>();

                    // Determine roles to be added and removed based on selected roles in the model
                    var rolesToAdd = selectedRoles.Except(currentRoles);
                    var rolesToRemove = currentRoles.Except(selectedRoles);

                    // Add and remove roles accordingly
                    var addToRolesResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
                    var removeFromRolesResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);

                    // Update password if provided
                    if (!string.IsNullOrEmpty(model.Password))
                    {
                        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                        var addPasswordResult = await _userManager.AddPasswordAsync(user, model.Password);

                        if (!removePasswordResult.Succeeded || !addPasswordResult.Succeeded)
                        {
                            // Handle password update failure
                            foreach (var error in removePasswordResult.Errors.Concat(addPasswordResult.Errors))
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                            return View(model);
                        }
                    }

                    // Check if role update and user update were successful
                    if (addToRolesResult.Succeeded && removeFromRolesResult.Succeeded)
                    {
                        // Redirect to Index on success
                        return RedirectToAction("Index");
                    }

                    // Handle role update failure
                    foreach (var error in addToRolesResult.Errors.Concat(removeFromRolesResult.Errors))
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    // Handle the case where the user is not found
                    return RedirectToAction("Index");
                }
            }

            // If the ModelState is not valid or the update fails, return to the view with the model
            //ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync() ?? new List<string>();
            ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();
            return View(model);
        }
        


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return RedirectToAction("Index");
            }

            var result = await _userManager.DeleteAsync(user);

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


        [HttpPost]
        public async Task<IActionResult> Delete2(string id){
            var user = await _userManager.FindByIdAsync(id);

            if(user != null){
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}