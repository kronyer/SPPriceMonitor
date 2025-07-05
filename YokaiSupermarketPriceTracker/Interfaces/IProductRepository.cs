using YokaiSupermarketPriceTracker.Infrastructure.Repositories;
using YokaiSupermarketPriceTracker.Models;

namespace YokaiSupermarketPriceTracker.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByBarcodeAsync(string barcode);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<ProductListDTO>> GetPagedAsync(int pageNumber, int pageSize, string searchTerm = "", DateTime? fromDate = null, DateTime? toDate = null);
    Task<int> AddAsync(Product product);
    Task<int> AddBrandAsync(Brand product);
    Task<int> GetBrandByName(string name);
    Task<int> AddStoreAsync(Store product);
    Task<int> GetStoreByName(string name);
    Task<int> DeleteAsync(string barcode);
}