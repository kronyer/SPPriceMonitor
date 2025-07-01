using SQLite;

namespace YokaiSupermarketPriceTracker.Models;

public class ProductVariation
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string ProductBarcode { get; set; } = string.Empty; // Foreign key

    public decimal Price { get; set; }

    public DateTime Date { get; set; }
}