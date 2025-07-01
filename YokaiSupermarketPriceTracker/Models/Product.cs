using SQLite;

namespace YokaiSupermarketPriceTracker.Models;

public class Product
{
    [PrimaryKey]
    public string Barcode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}