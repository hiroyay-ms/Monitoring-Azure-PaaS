using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class SalesOrderDetail
    {
        [Required]
        public int SalesOrderID { get; set; }
        [Required]
        public int SalesOrderDetailID { get; set; }
        [Required]
        public Int16 OrderQty { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal UnitPriceDiscount { get; set; }
        [Required]
        public decimal LineTotal { get; set; }
        [Required]
        public Guid rowguid { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}