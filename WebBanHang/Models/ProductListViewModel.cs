namespace WebBanHang.Models;

/// <summary>Dữ liệu trang danh sách sản phẩm (có tìm kiếm + lọc theo danh mục).</summary>
public class ProductListViewModel
{
    public const int DefaultPageSize = 12;
    public const int MaximumPageSize = 50;

    public IReadOnlyList<Product> Products { get; set; } = Array.Empty<Product>();

    public IReadOnlyList<Category> Categories { get; set; } = Array.Empty<Category>();

    /// <summary>Từ khóa khách đã nhập (giữ lại trên ô tìm kiếm).</summary>
    public string? Keyword { get; set; }

    /// <summary>Danh mục đang lọc, 0 = tất cả.</summary>
    public int CategoryId { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = DefaultPageSize;

    public int TotalCount { get; set; }

    public int TotalPages => TotalCount == 0
        ? 0
        : (int)(((long)TotalCount + PageSize - 1) / PageSize);

    public bool HasPreviousPage => Page > 1;

    public bool HasNextPage => Page < TotalPages;
}
