namespace Web.Models;

public class SalesOrderDetail
{
    public int SalesOrderID { get; set; } = 0;
    public int SalesOrderDetailID { get; set; } = 0;
    public int ProductID { get; set; } = 0;
    public string ProductName { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public Int16 OrderQty { get; set; } = 0;
    public decimal UnitPrice { get; set; } = 0;
    public decimal UnitPriceDiscount { get; set; } = 0;
    public decimal LineTotal { get; set; } = 0;
}
