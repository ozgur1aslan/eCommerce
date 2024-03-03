using eCommerce.Areas.Admin.ViewModels;
using eCommerce.Models;

namespace eCommerce.Data.Abstract
{
    public interface IVariantRepository{
        IQueryable<Variant> Variants {get;}

        Task<Variant> GetVariantByIdAsync(int? id);

        void CreateVariant(Variant variant);
        string ProcessAndSaveImage(IFormFile imageFile);

        void UpdateVariant(EditVariantViewModel variant);

        void UpdateVariant(EditVariantViewModel variant, int[] SelectedValues);

        void DeleteVariant(Variant variant);

    }
}