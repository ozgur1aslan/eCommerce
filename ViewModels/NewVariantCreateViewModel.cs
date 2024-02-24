using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.ViewModels
{
    

    public class NewVariantCreateViewModel
    {
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Stock { get; set; }

        public int? ProductId { get; set; }

        public List<NewValueCreateViewModel>? Values { get; set; } = new List<NewValueCreateViewModel>();
        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }


  
    

    public class NewValueCreateViewModel
    {

            public int ValueId { get; set; }
    }

}