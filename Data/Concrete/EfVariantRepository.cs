using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfVariantRepository : IVariantRepository
    {
        private eCommerceContext _context;
        public EfVariantRepository(eCommerceContext context){
            _context = context;
        }
        public IQueryable<Variant> Variants => _context.Variants;


        public async Task<Variant> GetVariantByIdAsync(int? id)
        {
            return await _context.Variants
                .FirstOrDefaultAsync(v => v.VariantId == id);
        }

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

        public void UpdateVariant(Variant variant, int[] SelectedValues)
        {
            var entity = _context.Variants

                .Include(i => i.Pictures)
                .Include(i => i.Values)


                .FirstOrDefault(i=>i.VariantId == variant.VariantId);

            entity.Values = _context.Values.Where(val => SelectedValues.Contains(val.ValueId)).ToList();

            _context.SaveChanges();
        }

        public void DeleteVariant(Variant variant)
        {
            _context.Variants.Remove(variant);
            _context.SaveChanges();
        }
    }
}