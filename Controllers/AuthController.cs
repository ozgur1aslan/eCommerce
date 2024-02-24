using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Controllers;

    public class AuthController  : Controller
{
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly EmailService _emailService;


        public AuthController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        


        public IActionResult Register(){
            return View();
        }
        

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if(ModelState.IsValid){
                var user = new AppUser{UserName = model.Email, Email = model.Email, FullName = model.FullName};
                IdentityResult result =  await _userManager.CreateAsync(user, model.Password);

                if(result.Succeeded){

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail","Auth", new{user.Id, token});


                    await _emailService.SendEmailAsync(
                        user.Email,
                        "Account Confirmation",
                        $"Please click the link in order to confirm your account: <a href='http://localhost:5087{url}'>Confirm.</a>"
                        );

                    TempData["message"] = "Email adresinize gelen onay linkine tıklayınız.";


                    return RedirectToAction("Login");
                }

                foreach(IdentityError err in result.Errors){
                    ModelState.AddModelError("",err.Description);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> ConfirmEmail(string Id, string token){
            if(Id == null || token == null){
                TempData["message"] = "Unvalid token";
                return View();
            }
            var user = await _userManager.FindByIdAsync(Id);

            if(user != null){
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded){
                    TempData["message"] = "Account confirmed";
                    return View();
                }
            }
            TempData["message"] = "No user found";
            return View();
        }



        public IActionResult ForgotPassword(){
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> ForgotPassword(string Email){
            if(string.IsNullOrEmpty(Email)){
                TempData ["message"] = "Please enter your Email address.";
                return View();
            }

            var user = await _userManager.FindByEmailAsync(Email);
            if(user == null){
                TempData ["message"] = "No user found with this Email";
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword","Auth", new {user.Id, token});

            await _emailService.SendEmailAsync(
                Email,
                "Reset Password",
                $"Please click this link in order to reset your password: <a href='http://localhost:5087{url}'></a>");


            TempData ["message"] = "You can change your password with the link that's been sent to your Email";
            return View();
        }



        public IActionResult ResetPassword(string Id, string token){
            if(Id == null || token == null){
                return RedirectToAction("Login");
            }

            var model = new ResetPasswordViewModel{Token = token};
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model){
            if(ModelState.IsValid){
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user == null){
                TempData ["message"] = "No user found with this Email";
                return RedirectToAction("Login");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if(result.Succeeded){
                TempData ["message"] = "Password changed";
               return RedirectToAction("Login");
            }
            }
            return View(model);
        }

}
