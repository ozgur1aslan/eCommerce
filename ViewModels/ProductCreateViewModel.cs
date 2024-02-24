using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    public class ProductCreateViewModel
    {
        public string ProductName { get; set; }

        [Range(1, 5, ErrorMessage = "The value must be between {1} and {2}.")]
        public bool? Rating { get; set; }

        public int? GenderId { get; set; }
        public int? CategoryId { get; set; }
        public int? SeasonId { get; set; }
        public int? BrandId { get; set; }


        public List<VariantCreateViewModel> Variants { get; set; } = new List<VariantCreateViewModel>();
    }

    public class VariantCreateViewModel
    {
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Stock { get; set; }



        public List<ValueCreateViewModel>? Values { get; set; } = new List<ValueCreateViewModel>();
        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }


  
    

    public class ValueCreateViewModel
    {

            public int ValueId { get; set; }
    }

}