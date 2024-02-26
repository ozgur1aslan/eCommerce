using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IOrderRepository
    {
        IQueryable<Order> Orders { get; }

        void CreateOrder(Order Order);
    }
}