using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfOrderRepository : IOrderRepository
    {
        private eCommerceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EfOrderRepository(eCommerceContext context, IWebHostEnvironment webHostEnvironment){
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IQueryable<Order> Orders => _context.Orders;



        public void CreateOrder(Order order)
        {
            try
            {
                // Save the product to the database
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error creating product: {ex.Message}");
                throw;
            }
        }

        



    }
}