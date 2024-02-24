namespace eCommerce.Models;

public enum TagColors
{
    primary, danger, warning, success, secondary, info
}
public class Tag
{
    public int TagId { get; set; }
    public string? Text { get; set; }
    public string? Url { get; set; }
    public TagColors? Color { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();   
}
