namespace eCommerce.Models
{
    public class Brand{
        public int BrandId { get; set; }
        public string BrandName {get;set;} = null!;
        public List<Product> Products {get;set;} = new List<Product>();
    }
}