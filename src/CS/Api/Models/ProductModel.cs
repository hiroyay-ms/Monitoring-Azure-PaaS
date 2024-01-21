using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ProductModel
    {
        [Required]
        public int ProductModelID { get; set; }
        [Required]
        public string Name { get; set; }
        public string CatalogDescription { get; set; } = null;
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}

