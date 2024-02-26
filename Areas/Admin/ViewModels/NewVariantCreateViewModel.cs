using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.Areas.Admin.ViewModels
{
    

    public class NewVariantCreateViewModel
    {
        
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Stock { get; set; }

        public int? ProductId { get; set; }

        public List<NewVariantValueViewModel>? Values { get; set; } = new List<NewVariantValueViewModel>();
        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }


  
    

    public class NewVariantValueViewModel
    {

            public int ValueId { get; set; }
    }

}