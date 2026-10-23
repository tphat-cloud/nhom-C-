namespace WebBanHang.Models;

/// <summary>Sản phẩm hiển thị cho khách (bảng Products + tên danh mục + tồn kho).</summary>
public class Product
{
    public int ProductId { get; set; }

    public string ProductCode { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? OriginalPrice { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = string.Empty;

    /// <summary>
    /// Ảnh đại diện của danh mục (bảng Categories). Chỉ dùng để hiển thị:
    /// nhiều sản phẩm trong CSDL chưa có ảnh riêng nên mượn ảnh danh mục cho đỡ trống.
    /// </summary>
    public string? CategoryImageUrl { get; set; }

    public bool IsFeatured { get; set; }

    /// <summary>Tổng tồn kho của mọi size/màu.</summary>
    public int TotalStock { get; set; }

    /// <summary>Chỉ được nạp ở trang chi tiết sản phẩm.</summary>
    public IReadOnlyList<ProductVariant> Variants { get; set; } = Array.Empty<ProductVariant>();

    public bool IsInStock => Variants.Count > 0
        ? Variants.Any(variant => variant.StockQuantity > 0)
        : TotalStock > 0;

    /// <summary>Phần trăm giảm giá, 0 nếu không có giá gốc lớn hơn giá bán.</summary>
    public int DiscountPercent => OriginalPrice is > 0 && OriginalPrice > Price
        ? (int)Math.Round((OriginalPrice.Value - Price) * 100 / OriginalPrice.Value)
        : 0;

    /// <summary>
    /// Ảnh hiển thị theo thứ tự ưu tiên: ảnh sản phẩm → ảnh danh mục → ảnh mặc định.
    /// </summary>
    public string DisplayImage => !string.IsNullOrWhiteSpace(ImageUrl)
        ? ImageUrl!
        : !string.IsNullOrWhiteSpace(CategoryImageUrl)
            ? CategoryImageUrl!
            : "/images/no-image.svg";

    /// <summary>Ảnh dự phòng khi ảnh chính bị lỗi tải (mất mạng) — dùng cho thuộc tính data-fallback.</summary>
    public string FallbackImage => string.IsNullOrWhiteSpace(CategoryImageUrl)
        ? "/images/no-image.svg"
        : CategoryImageUrl!;

    /// <summary>
    /// Nhãn tình trạng hàng cho khách xem. Cố ý <b>không</b> hiển thị số lượng tồn kho
    /// chính xác — đó là dữ liệu nội bộ, chỉ có trong ứng dụng quản lý.
    /// </summary>
    public string StockText => TotalStock <= 0
        ? "Hết hàng"
        : TotalStock <= 5
            ? "Sắp hết hàng"
            : "Còn hàng";
}
