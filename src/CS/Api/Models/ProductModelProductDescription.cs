using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class ProductModelProductDescription
    {
        [Required]
        public int ProductModelID { get; set; }
        [Required]
        public int ProductDescriptionID { get; set; }
        [Required]
        public string Culture { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}