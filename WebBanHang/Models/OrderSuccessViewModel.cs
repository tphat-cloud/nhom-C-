namespace WebBanHang.Models;

/// <summary>Thông tin hiển thị ở trang "Đặt hàng thành công".</summary>
public class OrderSuccessViewModel
{
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>Tổng tiền đã định dạng sẵn (VD: "567.000 đ") — lấy từ kết quả tạo đơn.</summary>
    public string TotalText { get; set; } = string.Empty;

    /// <summary>Tổng tiền dạng số (chưa định dạng) — trang xác nhận đẩy xuống JS để lưu vào LocalStorage.</summary>
    public decimal TotalAmount { get; set; }

    /// <summary>Thời điểm đơn được ghi nhận (giờ máy chủ) — dùng cho danh sách "Đơn hàng gần đây".</summary>
    public DateTime OrderDate { get; set; } = DateTime.Now;

    public string CustomerName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = PaymentMethods.Cod;

    /// <summary>Ghi chú hướng dẫn thanh toán theo từng phương thức.</summary>
    public string PaymentHint => PaymentMethod == PaymentMethods.BankTransfer
        ? "Cửa hàng sẽ gọi xác nhận và gửi thông tin chuyển khoản. Đơn hàng chỉ được giao sau khi nhận đủ tiền."
        : "Bạn thanh toán bằng tiền mặt cho nhân viên giao hàng khi nhận được hàng (COD).";
}
