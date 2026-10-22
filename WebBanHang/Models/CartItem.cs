namespace WebBanHang.Models;

/// <summary>
/// Một dòng trong giỏ hàng. Giỏ hàng lưu trong Session (xem Services/CartService),
/// nhưng giá và tên sản phẩm luôn được nạp lại từ CSDL khi thanh toán.
/// </summary>
public class CartItem
{
    public int ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    /// <summary>Ảnh hiển thị; dùng ảnh mặc định nếu sản phẩm chưa có ảnh.</summary>
    public string DisplayImage => string.IsNullOrWhiteSpace(ImageUrl)
        ? "/images/no-image.svg"
        : ImageUrl;
}
