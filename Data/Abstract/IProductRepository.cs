using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IProductRepository{
        IQueryable<Product> Products {get;}

        void CreateProduct(Product product);
        string ProcessAndSaveImage(IFormFile imageFile);


        void EditProduct(Product product);
        void EditProduct(Product product, int[] tagIds, bool x);
        void DeleteProduct(Product product);


    }
}