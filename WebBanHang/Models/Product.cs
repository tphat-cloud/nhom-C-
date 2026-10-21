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

    /// <summary>Ảnh hiển thị; dùng ảnh mặc định nếu sản phẩm chưa có ảnh.</summary>
    public string DisplayImage => string.IsNullOrWhiteSpace(ImageUrl)
        ? "/images/no-image.svg"
        : ImageUrl;

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
