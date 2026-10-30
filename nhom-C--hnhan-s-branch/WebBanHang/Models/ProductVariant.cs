namespace WebBanHang.Models;

/// <summary>Một biến thể size/màu kèm tồn kho (bảng ProductVariants).</summary>
public class ProductVariant
{
    public int ProductVariantId { get; set; }

    public int ProductId { get; set; }

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public int StockQuantity { get; set; }

    /// <summary>Còn hàng hay đã hết (dùng để khóa nút "Thêm vào giỏ" ở bước sau).</summary>
    public bool IsAvailable => StockQuantity > 0;
}
