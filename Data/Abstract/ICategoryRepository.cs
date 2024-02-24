using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface ICategoryRepository{
        IQueryable<Category> Categories {get;}

        void CreateCategory(Category category);

        void EditCategory(Category category);

        void DeleteCategory(Category category);
    }
}