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

    public async Task<IEnumerable<ProductListDTO>> GetPagedAsync(
        int pageNumber, int pageSize, string searchTerm = "", DateTime? fromDate = null, DateTime? toDate = null)
    {
        var products = await _db.Table<Product>().ToListAsync();
        var brands = await _db.Table<Brand>().ToListAsync();
        var variations = await _db.Table<ProductVariation>().ToListAsync();
        var stores = await _db.Table<Store>().ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
            products = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();

        if (fromDate.HasValue)
            variations = variations.Where(v => v.Date >= fromDate.Value).ToList();
        if (toDate.HasValue)
            variations = variations.Where(v => v.Date <= toDate.Value).ToList();

        var productDTOs = products
            .Select(p =>
            {
                var brand = brands.FirstOrDefault(b => b.Id == p.BrandId)?.Name ?? "";
                var latestVariation = variations
                    .Where(v => v.ProductBarcode == p.Barcode)
                    .OrderByDescending(v => v.Date)
                    .FirstOrDefault();
                var storeName = latestVariation != null
                    ? stores.FirstOrDefault(s => s.Id == latestVariation.StoreId)?.Name ?? ""
                    : "";

                return new ProductListDTO
                {
                    Barcode = p.Barcode,
                    Name = p.Name,
                    Brand = brand,
                    Price = latestVariation?.Price,
                    Store = storeName,
                    Date = latestVariation?.Date
                };
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return productDTOs;
    }

    public async Task<int> AddAsync(Product product)
    {
        return await _db.InsertOrReplaceAsync(product);
    }

    public async Task<int> AddBrandAsync(Brand brand)
    {
        return await _db.InsertOrReplaceAsync(brand);

    }

    public async Task<int> GetBrandByName(string name)
    {
        var brand = await _db.Table<Brand>().Where(b => b.Name == name).FirstOrDefaultAsync();
        return brand?.Id ?? 0;
    }

    public async Task<int> AddStoreAsync(Store store)
    {
        return await _db.InsertOrReplaceAsync(store);

    }

    public async Task<int> GetStoreByName(string name)
    {
        var store = await _db.Table<Store>().Where(s => s.Name == name).FirstOrDefaultAsync();
        return store?.Id ?? 0;
    }

    public async Task<int> DeleteAsync(string barcode)
    {
        return await _db.Table<Product>().DeleteAsync(p => p.Barcode == barcode);
    }
}