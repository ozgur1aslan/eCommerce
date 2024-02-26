using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.Data.Abstract;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using eCommerce.Data.Concrete;

namespace eCommerce.Controllers;

public class CheckoutController : Controller
{

    private readonly ICartRepository _cartRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    private readonly EmailService _emailService;


    public CheckoutController(IOrderRepository orderRepository, ICartRepository cartRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, EmailService emailService)
    {
        _cartRepository = cartRepository;
        _orderRepository = orderRepository;
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

        if (user == null)
        {
            return NotFound();
        }


        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;


        ViewBag.CartItems = _cartRepository.CartItems
        .Include(i => i.Variant)
            .ThenInclude(i => i.Product)
        .Include(i => i.Variant)
            .ThenInclude(i => i.Pictures)
        .Include(i => i.Variant)
            .ThenInclude(i => i.Values)
        .Where(item => item.UserId == userId).ToList();

        var orderVM = new OrderCreateViewModel
        {
            TotalPrice = totalPrice,
            UserId = userId
        };

        if (user.Address != null)
        {
            orderVM.Address = user.Address;
        }

        if (user.CardNumber != null)
        {
            orderVM.CardNumber = user.CardNumber;
        }


        return View(orderVM);
    }


    [HttpPost]
    public IActionResult PaymentPOST(OrderCreateViewModel orderVM)
    {

        if (ModelState.IsValid)
        {

            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var cartItems = _cartRepository.CartItems
            .Include(i => i.Variant)
                .ThenInclude(i => i.Product)
            .Include(i => i.Variant)
                .ThenInclude(i => i.Pictures)
            .Include(i => i.Variant)
                .ThenInclude(i => i.Values)
            .Where(item => item.UserId == userId).ToList();


            var order = new Order
            {
                UserId = orderVM.UserId,
                TotalPrice = orderVM.TotalPrice,
                PurchaseDate = DateTime.Now,
                OrderStatus = "Pending shipment.",
                Address = orderVM.Address,
                CardNumber = orderVM.CardNumber
            };





            try
            {
                foreach (var item in cartItems)
                {
                    var purchasedItem = new PurchasedItem
                    {
                        VariantId = item.VariantId,
                        Quantity = item.Quantity
                    };

                    order.PurchasedItems.Add(purchasedItem);

                    _cartRepository.RemoveFromCart(item.VariantId, userId);
                }


                _orderRepository.CreateOrder(order);


                return RedirectToAction("ThankYou");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                ModelState.AddModelError(string.Empty, "An error occurred while processing the request.");
                // You may want to log the exception for further investigation
                // Logging.Log(ex);
            }



        }


        return View();

    }



    public IActionResult ThankYou()
    {




        return View();
    }



}
