namespace WebBanHang.Models;

/// <summary>Dữ liệu trang chủ: danh mục + sản phẩm nổi bật lấy trực tiếp từ SQL Server.</summary>
public class HomeViewModel
{
    public IReadOnlyList<Category> Categories { get; set; } = Array.Empty<Category>();

    public IReadOnlyList<Product> FeaturedProducts { get; set; } = Array.Empty<Product>();

    public int TotalProducts { get; set; }
}
