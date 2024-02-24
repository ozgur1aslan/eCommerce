using eCommerce.Areas.User.ViewModels;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Areas.Admin.Controllers
{
    [Area("User")]
    public class AccountController:Controller{


        private UserManager<AppUser> _userManager;
        private readonly eCommerceContext _context;

        private readonly EmailService _emailService;


        public AccountController(eCommerceContext context, UserManager<AppUser> userManager, EmailService emailService){

            _context = context;
            
            _userManager = userManager;

            _emailService = emailService;
        }



        public async Task<IActionResult> Profile(string id)
        {
            if(id==null){
                return RedirectToAction("Index");
            }
            var user = await _userManager.FindByIdAsync(id);

            ViewBag.UserGenders = new SelectList(await _context.UserGenders.ToListAsync(), "UserGenderName", "UserGenderName");

            if(user != null){

                return View(new EditDetailsViewModel
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    UserGender = user.UserGender,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    CardNumber = user.CardNumber,
                    DateOfBirth = user.DateOfBirth
                });
                }

            
                
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Profile(string id, EditDetailsViewModel model)
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
                    user.PhoneNumber = model.PhoneNumber;
                    user.Address = model.Address;
                    user.CardNumber = model.CardNumber;
                    user.DateOfBirth = model.DateOfBirth;
                    user.UserGender = model.UserGender;


                    // Confirm password
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

                    
                
                }
                else
                {
                    // Handle the case where the user is not found
                    return RedirectToAction("Index");
                }
            }

            ViewBag.UserGenders = new SelectList(await _context.UserGenders.ToListAsync(), "UserGenderName", "UserGenderName");
            
            return View(model);
        }



        



    }
}