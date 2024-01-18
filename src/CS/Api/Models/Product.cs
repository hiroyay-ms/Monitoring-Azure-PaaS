using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Product
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        public string Color { get; set; } = null;
        [Required]
        public decimal StandardCost { get; set; }
        [Required]
        public decimal ListPrice {get; set;}
        public string Size {get; set;} = null;
        public decimal? Weight {get; set;} = null;
        public int? ProductCategoryID {get; set;} = null;
        public int? ProductModelID {get; set;} = null;
        [Required]
        public DateTime SellStartDate {get; set;}
        public DateTime? SellEndDate {get; set;}
        public DateTime? DiscontinuedDate {get; set;}
        public byte[] ThumbNailPhoto {get ;set;} = null;
        public string ThumbnailPhotoFileName {get; set;} = null;
        [Required]
        public Guid rowguid {get; set;}
        [Required]
        public DateTime ModifiedDate {get; set;}
    }
}
