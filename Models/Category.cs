namespace eCommerce.Models
{
    public class Category{
        public int CategoryId { get; set; }
        public string CategoryName {get;set;} = null!;
        public List<Product> Products {get;set;} = new List<Product>();
    }
}