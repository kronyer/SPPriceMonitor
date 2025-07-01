using YokaiSupermarketPriceTracker.Models;

namespace YokaiSupermarketPriceTracker.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<int> AddAsync(Product product);
    Task<int> DeleteAsync(string barcode);
}