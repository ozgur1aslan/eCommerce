using eCommerce.Areas.Admin.ViewModels;
using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfVariantRepository : IVariantRepository
    {
        private eCommerceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EfVariantRepository(eCommerceContext context, IWebHostEnvironment webHostEnvironment){
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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

        public void UpdateVariant(EditVariantViewModel variant)
        {
//            _context.Variants.Update(variant);
            _context.SaveChanges();
        }

        public void UpdateVariant(EditVariantViewModel variantVM, int[] SelectedValues)
        {
            var entity = _context.Variants
                .Include(v => v.Product)
                    .ThenInclude(p => p.Gender)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Season)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Brand)
                .Include(v => v.Product)
                    .ThenInclude(p => p.Category)
                .Include(v => v.Pictures)
                .Include(v => v.Values)
                    .ThenInclude(v => v.Option)
                .FirstOrDefault(i => i.VariantId == variantVM.VariantId);



            // Delete existing images associated with the variant
            foreach (var picture in entity.Pictures.ToList())
            {
                DeleteImageFile(picture.Path);
                entity.Pictures.Remove(picture);
            }


            foreach (var pictureFile in variantVM.Pictures)
            {
                var imageUrl = ProcessAndSaveImage(pictureFile);
                entity.Pictures.Add(new Picture { Path = imageUrl });
            }


            // Update the variant values
            entity.Values = _context.Values.Where(val => SelectedValues.Contains(val.ValueId)).ToList();

            entity.Price = variantVM.Price;
            entity.DiscountedPrice = variantVM.DiscountedPrice;
            entity.Stock = variantVM.Stock;
            

            _context.SaveChanges();
        }

        private void DeleteImageFile(string imagePath)
        {
            try
            {
                // Combine the web root path with the image path to get the full file path
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));

                // Check if the file exists before attempting to delete
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting image file: {ex.Message}");
                throw;
            }
        }




        public void DeleteVariant(Variant variant)
        {

            foreach (var picture in variant.Pictures.ToList())
            {
                DeleteImageFile(picture.Path);
            }

            _context.Variants.Remove(variant);
            _context.SaveChanges();
        }




        public string ProcessAndSaveImage(IFormFile imageFile)
        {
            try
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    string uniqueFileName = GetUniqueFileName2(imageFile.FileName);
                    string directoryPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    // Ensure the directory exists
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string filePath = Path.Combine(directoryPath, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        imageFile.CopyTo(fileStream);
                    }

                    return $"images/{uniqueFileName}";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing and saving image: {ex.Message}");
                throw;
            }

            return null;
        }

        private string GetUniqueFileName2(string fileName)
        {
            // Use a GUID to generate a unique filename
            string guid = Path.GetRandomFileName();
            return $"{guid}_{fileName}";
        }
    }
}