using SQLite;

namespace YokaiSupermarketPriceTracker.Models;

public class Store
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}