namespace eCommerce.Models
{
    public class Gender{
        public int GenderId { get; set; }
        public string GenderName {get;set;} = null!;
        
        public List<Product> Products {get;set;} = new List<Product>();
    }
}