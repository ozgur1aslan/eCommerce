using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set;} = null!;

        public string? Description { get; set; }
        public bool isActive { get; set; } = true;

        public int? GenderId { get; set; }
        public Gender? Gender { get; set; }

        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public int? SeasonId { get; set; }
        public Season? Season { get; set; }

        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }


        public List<Tag>? Tags {get;set;} = new();




    }
}