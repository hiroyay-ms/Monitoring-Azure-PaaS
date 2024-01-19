using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ProductCategory
    {
        [Required]
        public int ProductCategoryID { get; set; }
        public int? ParentProductCategoryID { get; set; } = null;
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
