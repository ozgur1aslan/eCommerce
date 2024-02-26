using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Controllers;

    public class CheckoutController  : Controller
{
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly EmailService _emailService;


        public CheckoutController (UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> PaymentAsync(decimal totalPrice)
        {
            var user = await _userManager.GetUserAsync(User);

            
            return View();
        }


}
