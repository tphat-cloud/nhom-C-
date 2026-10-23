using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models;

/// <summary>
/// Model cho trang "Tra cứu đơn hàng" (tìm theo Mã đơn + Số điện thoại).
/// </summary>
public class OrderLookupViewModel
{
    // ---- Thông tin nhập tra cứu ----
    [Display(Name = "Mã đơn hàng")]
    public string? OrderCodeInput { get; set; }

    [Display(Name = "Số điện thoại")]
    public string? PhoneNumberInput { get; set; }

    /// <summary>Đã thực hiện bấm nút tra cứu hay chưa.</summary>
    public bool HasSearched { get; set; }

    /// <summary>Có tìm thấy đơn hàng khớp hay không.</summary>
    public bool IsFound { get; set; }

    /// <summary>Thông báo lỗi khi không tìm thấy hoặc nhập sai định dạng.</summary>
    public string? ErrorMessage { get; set; }

    // ---- Thông tin đơn hàng (khi tìm thấy) ----
    public int OrderId { get; set; }

    public string OrderCode { get; set; } = string.Empty;

    public string CustomerName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string ShippingAddress { get; set; } = string.Empty;

    public string? Note { get; set; }

    public string Status { get; set; } = OrderStatuses.Pending;

    public string PaymentMethod { get; set; } = PaymentMethods.Cod;

    public bool IsPaid { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public IReadOnlyList<OrderLookupItemViewModel> Items { get; set; } = Array.Empty<OrderLookupItemViewModel>();

    // ---- Luồng trạng thái & tiện ích hiển thị ----
    public static IReadOnlyList<string> StatusFlow { get; } = new[]
    {
        OrderStatuses.Pending,
        OrderStatuses.Approved,
        OrderStatuses.Shipping,
        OrderStatuses.Completed
    };

    /// <summary>Đơn đã hủy thì không hiển thị thanh tiến trình 4 bước thông thường.</summary>
    public bool IsCancelled => Status == OrderStatuses.Cancelled;

    /// <summary>Bước hiện tại (1..4) trong luồng giao hàng. Trả về 0 nếu đã hủy hoặc không rõ.</summary>
    public int CurrentStep
    {
        get
        {
            if (IsCancelled) return 0;
            int idx = Array.IndexOf(StatusFlow.ToArray(), Status);
            return idx >= 0 ? idx + 1 : 1;
        }
    }

    /// <summary>Mô tả giải thích chi tiết cho khách hàng về trạng thái hiện tại.</summary>
    public string StatusDescription => Status switch
    {
        OrderStatuses.Pending => "Đơn hàng đã được lưu vào hệ thống. Cửa hàng sẽ sớm liên hệ xác nhận đơn qua số điện thoại của bạn.",
        OrderStatuses.Approved => "Đơn hàng đã được xác nhận. Nhân viên đang đóng gói sản phẩm để bàn giao cho đơn vị vận chuyển.",
        OrderStatuses.Shipping => "Đơn hàng đang trên đường vận chuyển tới địa chỉ nhận. Vui lòng chú ý điện thoại từ shipper.",
        OrderStatuses.Completed => "Đơn hàng đã được giao thành công. Cảm ơn bạn đã mua sắm và tin tưởng Fashion Shop!",
        OrderStatuses.Cancelled => "Đơn hàng đã bị hủy. Nếu có bất kỳ thắc mắc nào, vui lòng liên hệ hotline 1900 6868.",
        _ => "Đơn hàng đang trong quá trình xử lý."
    };

    public string StatusBadgeClass => Status switch
    {
        OrderStatuses.Pending => "pill pill--warning",
        OrderStatuses.Approved => "pill pill--info",
        OrderStatuses.Shipping => "pill pill--primary",
        OrderStatuses.Completed => "pill pill--success",
        OrderStatuses.Cancelled => "pill pill--danger",
        _ => "pill pill--low"
    };

    public string PaymentMethodText => PaymentMethod switch
    {
        PaymentMethods.BankTransfer => "Chuyển khoản ngân hàng",
        _ => "Thanh toán khi nhận hàng (COD)"
    };
}

/// <summary>Một món hàng trong đơn được hiển thị khi tra cứu.</summary>
public class OrderLookupItemViewModel
{
    public string ProductName { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Color { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal TotalPrice { get; set; }

    public string? ImageUrl { get; set; }

    public string DisplayImage => !string.IsNullOrWhiteSpace(ImageUrl)
        ? ImageUrl!
        : "/images/no-image.svg";
}
