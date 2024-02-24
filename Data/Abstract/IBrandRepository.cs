using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IBrandRepository{
        IQueryable<Brand> Brands {get;}

        void CreateBrand(Brand brand);

        void UpdateBrand(Brand brand);

        void DeleteBrand(Brand brand);
    }
}