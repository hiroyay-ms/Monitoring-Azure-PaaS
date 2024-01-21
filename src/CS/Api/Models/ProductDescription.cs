using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ProductDescription
    {
        [Required]
        public int ProductDescriptionID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}