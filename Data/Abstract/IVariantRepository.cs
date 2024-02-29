using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IVariantRepository{
        IQueryable<Variant> Variants {get;}

        Task<Variant> GetVariantByIdAsync(int? id);

        void CreateVariant(Variant variant);

        void UpdateVariant(Variant variant);

        void UpdateVariant(Variant variant, int[] SelectedValues);

        void DeleteVariant(Variant variant);

    }
}