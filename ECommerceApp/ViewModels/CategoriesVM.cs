using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.ViewModels
{
    public class CategoriesVM
    {
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
    }
}