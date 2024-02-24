using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;

namespace eCommerce.Data.Concrete
{
    public class EfCategoryRepository : ICategoryRepository
    {
        private eCommerceContext _context;
        public EfCategoryRepository(eCommerceContext context){
            _context = context;
        }
        public IQueryable<Category> Categories => _context.Categories;

        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void EditCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }
    }
}