using SQLite;

namespace YokaiSupermarketPriceTracker.Models;

public class Product
{
    [PrimaryKey]
    public string Barcode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int BrandId { get; set; } // Foreign key to Brand
    [Ignore]
    public Brand Brand { get; set; }
    
}

public class Brand
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}