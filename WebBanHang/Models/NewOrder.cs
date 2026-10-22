namespace WebBanHang.Models;

/// <summary>
/// Yêu cầu tạo đơn hàng do tầng Controller gửi xuống tầng dữ liệu.
/// Chỉ chứa mã biến thể + số lượng; giá và tên sản phẩm do CSDL quyết định.
/// </summary>
public class NewOrder
{
    public string CustomerName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string Address { get; set; } = string.Empty;

    public string? Note { get; set; }

    /// <summary>COD hoặc Chuyển khoản (khớp ràng buộc CK_Orders_PaymentMethod).</summary>
    public string PaymentMethod { get; set; } = "COD";

    public IReadOnlyList<NewOrderItem> Items { get; set; } = Array.Empty<NewOrderItem>();
}

/// <summary>Một dòng hàng cần đặt.</summary>
public class NewOrderItem
{
    public int ProductVariantId { get; set; }

    public int Quantity { get; set; }
}
