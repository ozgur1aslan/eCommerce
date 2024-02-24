namespace eCommerce.Models
{
    public class Season{
        public int SeasonId { get; set; }
        public string SeasonName {get;set;} = null!;
        
        public List<Product> Products {get;set;} = new List<Product>();
    }
}