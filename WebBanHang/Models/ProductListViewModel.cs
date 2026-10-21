namespace WebBanHang.Models;

/// <summary>Dữ liệu trang danh sách sản phẩm (có tìm kiếm + lọc theo danh mục).</summary>
public class ProductListViewModel
{
    public IReadOnlyList<Product> Products { get; set; } = Array.Empty<Product>();

    public IReadOnlyList<Category> Categories { get; set; } = Array.Empty<Category>();

    /// <summary>Từ khóa khách đã nhập (giữ lại trên ô tìm kiếm).</summary>
    public string? Keyword { get; set; }

    /// <summary>Danh mục đang lọc, 0 = tất cả.</summary>
    public int CategoryId { get; set; }

    public int TotalCount => Products.Count;
}
