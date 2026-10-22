using WebBanHang.Models;

namespace WebBanHang.Data;

/// <summary>
/// Ghi đơn hàng vào CSDL. Toàn bộ (khách + đơn + chi tiết + trừ kho) nằm trong
/// MỘT transaction: hoặc lưu đủ, hoặc không lưu gì.
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Tạo đơn hàng từ giỏ của khách.
    /// Giá và tên sản phẩm luôn đọc lại từ CSDL (không tin dữ liệu client gửi lên).
    /// </summary>
    Task<OrderResult> CreateOrderAsync(NewOrder order, CancellationToken cancellationToken = default);
}
