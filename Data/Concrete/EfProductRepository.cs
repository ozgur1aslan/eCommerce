using eCommerce.Data.Abstract;
using eCommerce.Data.Concrete.EfCore;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private eCommerceContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EfProductRepository(eCommerceContext context, IWebHostEnvironment webHostEnvironment){
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IQueryable<Product> Products => _context.Products;



        public void CreateProduct(Product product)
        {
            try
            {
                // Save the product to the database
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.WriteLine($"Error creating product: {ex.Message}");
                throw;
            }
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


        





        public void EditProduct(Product product)
        {
            if(product != null){
                _context.Products.Update(product);
                _context.SaveChanges();
            }
        }

        public void EditProduct(Product product, int[] tagIds, bool x)
        {

            var entity = _context.Products.Include(i=>i.Tags).FirstOrDefault(i=>i.ProductId == product.ProductId);

            if(entity != null){
                entity.ProductId = product.ProductId;
                entity.ProductName = product.ProductName;
                entity.isActive = x;
                

                if(product.Description != null)
                {
                    entity.Description = product.Description;
                }
                

                if(product.CategoryId != null)
                {
                    entity.CategoryId = product.CategoryId;
                }

                if(product.SeasonId != null)
                {
                    entity.SeasonId = product.SeasonId;
                }

                if(product.BrandId != null)
                {
                    entity.BrandId = product.BrandId;
                }

                if(product.GenderId != null)
                {
                    entity.GenderId = product.GenderId;
                }

                entity.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();
                _context.SaveChanges();
            }  
        }


        public void DeleteProduct(Product product)
        {
            if(product != null){
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }







    }
}