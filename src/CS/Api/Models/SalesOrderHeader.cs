using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class SalesOrderHeader
    {
        [Required]
        public int SalesOrderID { get; set; }
        [Required]
        public byte RevisionNumber { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ShipDate { get; set; } = null;
        [Required]
        public byte Status { get; set; }
        [Required]
        public bool OnlineOrderFlag { get; set; }
        [Required]
        public string SalesOrderNumber { get; set; }
        public string PurchaseOrderNumber { get; set; } = null;
        public string AccountNumber { get; set; } = null;
        [Required]
        public int CustomerID { get; set; }
        public int? ShipToAddressID { get; set; } = null;
        public int? BillToAddressID { get; set; } = null;
        [Required]
        public string ShipMethod { get; set; }
        public string CreditCardApprovalCode { get; set; } = null;
        [Required]
        public decimal SubTotal { get; set; }
        [Required]
        public decimal TaxAmt { get; set; }
        [Required]
        public decimal Freight { get; set; }
        [Required]
        public decimal TotalDue { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}