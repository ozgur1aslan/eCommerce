using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IVariantRepository{
        IQueryable<Variant> Variants {get;}

        void CreateVariant(Variant variant);

        void UpdateVariant(Variant variant);

        void DeleteVariant(Variant variant);
    }
}