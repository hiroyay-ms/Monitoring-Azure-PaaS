namespace Web.Models;

public class SalesOrder
{
    public int SalesOrderID { get; set; } = 0;
    public DateTime? ShipDate { get; set; } = null;
    public string SalesOrderNumber { get; set; } = string.Empty;
    public string? AccountNumber { get; set; } = null;
    public int CustomerID { get; set; } = 0;
    public string CustomerName { get; set; } = string.Empty;
    public string? CompanyName { get; set; } = null;
    public int? ShipToAddressID { get; set; } = null;
    public string ShipMethod { get; set; } = string.Empty;
    public string AddressLine1 { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string StateProvince { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public decimal SubTotal { get; set; } = 0;
    public decimal TaxAmt { get; set; } = 0;
    public decimal Freight { get; set; } = 0;
    public decimal TotalDue { get; set; } = 0;
}
