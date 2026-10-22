namespace WebBanHang.Models;

/// <summary>
/// Thông tin một biến thể size/màu đọc trực tiếp từ CSDL.
/// Dùng khi khách bấm "Thêm vào giỏ" để lấy giá/tên thật, không tin dữ liệu client gửi lên.
/// </summary>
public class VariantInfo
{
    public int ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsAvailable => StockQuantity > 0;

    public CartItem ToCartItem(int quantity) => new()
    {
        ProductVariantId = ProductVariantId,
        ProductId = ProductId,
        ProductName = ProductName,
        Size = Size,
        Color = Color,
        UnitPrice = Price,
        Quantity = quantity,
        ImageUrl = ImageUrl
    };
}
