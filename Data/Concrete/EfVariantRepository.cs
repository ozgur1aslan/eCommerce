using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;

namespace eCommerce.Data.Concrete
{
    public class EfVariantRepository : IVariantRepository
    {
        private eCommerceContext _context;
        public EfVariantRepository(eCommerceContext context){
            _context = context;
        }
        public IQueryable<Variant> Variants => _context.Variants;

        public void CreateVariant(Variant variant)
        {
            _context.Variants.Add(variant);
            _context.SaveChanges();
        }

        public void UpdateVariant(Variant variant)
        {
            _context.Variants.Update(variant);
            _context.SaveChanges();
        }

        public void DeleteVariant(Variant variant)
        {
            _context.Variants.Remove(variant);
            _context.SaveChanges();
        }
    }
}