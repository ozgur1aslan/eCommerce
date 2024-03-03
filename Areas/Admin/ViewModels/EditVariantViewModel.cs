using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using eCommerce.Models;

namespace eCommerce.Areas.Admin.ViewModels
{
    

    public class EditVariantViewModel
    {
        public int VariantId { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountedPrice { get; set; }
        public int Stock { get; set; }

        public int? ProductId { get; set; }

        public List<Value>? Values { get; set; } = new List<Value>();
        public List<IFormFile> Pictures { get; set; } = new List<IFormFile>();
    }


  
    

}