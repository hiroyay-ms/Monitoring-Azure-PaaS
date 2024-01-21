using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Address
    {
        [Required]
        public int AddressID { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; } = null;
        [Required]
        public string City { get; set; }
        [Required]
        public string StateProvince { get; set; }
        [Required]
        public string CountryRegion { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}