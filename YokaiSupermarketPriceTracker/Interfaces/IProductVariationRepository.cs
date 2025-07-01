using YokaiSupermarketPriceTracker.Models;

namespace YokaiSupermarketPriceTracker.Interfaces;

public interface IProductVariationRepository
{
    Task<ProductVariation> GetByIdAsync(int id);
    Task<IEnumerable<ProductVariation>> GetByProductBarcodeAsync(string barcode);
    Task<int> AddAsync(ProductVariation variation);
    Task<int> DeleteAsync(int id);
}