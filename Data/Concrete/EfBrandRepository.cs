using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;

namespace eCommerce.Data.Concrete
{
    public class EfBrandRepository : IBrandRepository
    {
        private eCommerceContext _context;
        public EfBrandRepository(eCommerceContext context){
            _context = context;
        }
        public IQueryable<Brand> Brands => _context.Brands;

        public void CreateBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
        }

        public void UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
            _context.SaveChanges();
        }

        public void DeleteBrand(Brand brand)
        {
            _context.Brands.Remove(brand);
            _context.SaveChanges();
        }
    }
}