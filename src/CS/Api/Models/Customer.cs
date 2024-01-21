using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Customer
    {
        [Required]
        public int CustomerID { get; set; }
        [Required]
        public bool NameStyle { get; set; }
        public string Title { get; set; } = null;
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; } = null;
        [Required]
        public string LastName { get; set; }
        public string Suffix { get; set; } = null;
        public string CompanyName { get; set; } = null;
        public string SalesPerson { get; set; } = null;
        public string EmailAddress { get; set; } = null;
        public string Phone { get; set; } = null;
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordSalt { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}