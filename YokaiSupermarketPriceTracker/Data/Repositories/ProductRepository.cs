using SQLite;
using YokaiSupermarketPriceTracker.Interfaces;
using YokaiSupermarketPriceTracker.Models;

namespace YokaiSupermarketPriceTracker.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SQLiteAsyncConnection _db;

    public ProductRepository(AppDbContext context)
    {
        _db = context.Database;
    }
    
    public async Task<Product?> GetByBarcodeAsync(string barcode)
    {
        return await _db.Table<Product>().Where(p => p.Barcode == barcode).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _db.Table<Product>().ToListAsync().ContinueWith(t => (IEnumerable<Product>)t.Result);
    }

    public async Task<int> AddAsync(Product product)
    {
        return await _db.InsertOrReplaceAsync(product);
    }

    public async Task<int> DeleteAsync(string barcode)
    {
        return await _db.Table<Product>().DeleteAsync(p => p.Barcode == barcode);
    }
}