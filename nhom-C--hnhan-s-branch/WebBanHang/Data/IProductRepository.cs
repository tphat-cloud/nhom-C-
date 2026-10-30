using WebBanHang.Models;

namespace WebBanHang.Data;

/// <summary>
/// Truy vấn dữ liệu sản phẩm / danh mục cho giao diện web.
/// Toàn bộ câu lệnh dùng tham số hóa (@param) để chống SQL Injection.
/// </summary>
public interface IProductRepository
{
    Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);

    Task<int> CountActiveProductsAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int take = 8, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetProductsAsync(
        string? keyword,
        int categoryId,
        CancellationToken cancellationToken = default);

    Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ProductVariant>> GetVariantsAsync(int productId, CancellationToken cancellationToken = default);

    Task<Product?> GetProductWithVariantsAsync(int productId, CancellationToken cancellationToken = default);
}
