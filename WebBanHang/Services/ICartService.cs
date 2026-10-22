using WebBanHang.Models;

namespace WebBanHang.Services;

/// <summary>
/// Giỏ hàng của khách, lưu trong Session (không cần đăng nhập).
/// Mọi thao tác đọc/ghi đều thông qua service này để tránh trùng lặp logic.
/// </summary>
public interface ICartService
{
    /// <summary>Số lượng tối đa cho mỗi biến thể (chặn khách nhập số vô lý).</summary>
    const int MaxQuantityPerItem = 99;

    IReadOnlyList<CartItem> GetItems();

    int TotalQuantity { get; }

    decimal TotalAmount { get; }

    /// <summary>Thêm hàng; nếu biến thể đã có trong giỏ thì cộng dồn số lượng.</summary>
    void Add(CartItem item, int quantity);

    /// <summary>Cập nhật số lượng; quantity &lt;= 0 nghĩa là xóa dòng đó.</summary>
    void UpdateQuantity(int productVariantId, int quantity);

    void Remove(int productVariantId);

    void Clear();
}
