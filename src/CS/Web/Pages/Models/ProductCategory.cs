namespace Web.Models;

public class ProductCategory
{
    public int ProductCategoryID { get; set; } = 0;
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public int ProductCount { get; set; } = 0;
}
