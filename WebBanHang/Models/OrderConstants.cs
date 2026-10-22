namespace WebBanHang.Models;

/// <summary>
/// Các phương thức thanh toán hệ thống hỗ trợ.
/// Giá trị phải khớp đúng ràng buộc CK_Orders_PaymentMethod trong CSDL.
/// </summary>
public static class PaymentMethods
{
    public const string Cod = "COD";

    public const string BankTransfer = "Chuyển khoản";

    public static readonly IReadOnlyList<string> All = new[] { Cod, BankTransfer };

    public static bool IsSupported(string? method) =>
        !string.IsNullOrWhiteSpace(method) && All.Contains(method);
}

/// <summary>
/// Trạng thái đơn hàng theo luồng nghiệp vụ.
/// Giá trị phải khớp đúng ràng buộc CK_Orders_Status trong CSDL.
/// </summary>
public static class OrderStatuses
{
    public const string Pending = "Chờ xử lý";

    public const string Approved = "Đã duyệt";

    public const string Shipping = "Đang giao";

    public const string Completed = "Hoàn thành";

    public const string Cancelled = "Đã hủy";
}
