using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace eCommerce.Models
{
    public class Product
        {
            public int ProductId { get; set; }
            [Required]
            public string ProductName { get; set; }
            
            [Range(1, 5, ErrorMessage = "The value must be between {1} and {2}.")]
            public bool? Rating { get; set; }

            public string? Description { get; set; }
            public bool isActive { get; set; } = true;

            public int? GenderId { get; set; }
            public Gender Gender { get; set; } = null!;
            public int? CategoryId { get; set; }
            public Category Category { get; set; } = null!;

            public int? SeasonId { get; set; }
            public Season Season { get; set; } = null!;

            public int? BrandId { get; set; }
            public Brand Brand { get; set; } = null!;



            public List<Tag> Tags { get; set; } = new List<Tag>();
            public List<Variant> Variants { get; set; } = new List<Variant>();

            public List<Comment>? Comments { get; set; } = new List<Comment>();
        }

        // Product Variant Model
        public class Variant
        {
            public int VariantId { get; set; }
            public decimal Price { get; set; }
            public decimal? DiscountedPrice { get; set; }
            
            public int Stock { get; set; }

            public int? ProductId { get; set; }
            public Product? Product { get; set; } = null!;
            

            public List<Value>? Values { get; set; } = new List<Value>();

            public List<Picture>? Pictures { get; set; } = new List<Picture>();

            
        }

        public class Picture
        {
            public int PictureId { get; set; }
            public string? Path { get; set; }

            // Foreign key to associate the image with a variant
            public int VariantId { get; set; }
            public Variant Variant { get; set; } = null!;

            [NotMapped] // This attribute is used to indicate that this property is not mapped to a database column
            [Display(Name = "Image File")]
            public IFormFile ImageFile { get; set; } = null!;
        }

        // Option Model
        public class Option
        {
            public int OptionId { get; set; }
            public string? OptionName { get; set; }

            [JsonIgnore]
            public List<Value>? Values { get; set; } = new List<Value>();

        }

        public class Value
        {
            public int ValueId { get; set; }
            public string? ValueText { get; set; }
            public int? OptionId { get; set; }
            public Option? Option { get; set; } = null!;


            public List<Variant>? Variants { get; set; } = new List<Variant>();
        }
}