namespace Web.Models;

public class Product
{
    public int ProductID { get; set; } = 0;
    public string? ProductName { get; set; }
    public string? ProductNumber { get; set; }
    public string? Color { get; set; }
    public decimal StandardCost { get; set; } = 0;
    public decimal ListPrice { get; set; } = 0;
    public string? Size { get; set; }
    public decimal? Weight { get; set; } = 0;
    public int ProductCategoryID { get; set; } = 0;
    public string? CategoryName { get; set; }
    public string? ModelName { get; set; }
    public string? Description { get; set; }
    public DateTime SellStartDate { get; set; } = DateTime.MinValue;
    public DateTime? SellEndDate { get; set; }
    public string? ThumbnailPhotoFileName { get; set; }
}
