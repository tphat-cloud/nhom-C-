using System.ComponentModel.DataAnnotations;

namespace WebBanHang.Models;

/// <summary>Dữ liệu form đặt hàng của khách + tóm tắt giỏ hàng để hiển thị lại.</summary>
public class CheckoutViewModel
{
    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Vui lòng nhập họ tên người nhận.")]
    [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
    public string CustomerName { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại để cửa hàng liên hệ.")]
    [StringLength(20, ErrorMessage = "Số điện thoại tối đa 20 ký tự.")]
    [RegularExpression(@"^0\d{9,10}$",
        ErrorMessage = "Số điện thoại không hợp lệ (bắt đầu bằng 0, gồm 10-11 chữ số).")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Display(Name = "Email (không bắt buộc)")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
    [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự.")]
    public string? Email { get; set; }

    [Display(Name = "Địa chỉ nhận hàng")]
    [Required(ErrorMessage = "Vui lòng nhập địa chỉ nhận hàng.")]
    [StringLength(255, ErrorMessage = "Địa chỉ tối đa 255 ký tự.")]
    public string Address { get; set; } = string.Empty;

    [Display(Name = "Ghi chú cho cửa hàng")]
    [StringLength(500, ErrorMessage = "Ghi chú tối đa 500 ký tự.")]
    public string? Note { get; set; }

    [Display(Name = "Phương thức thanh toán")]
    [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán.")]
    public string PaymentMethod { get; set; } = PaymentMethods.Cod;

    /// <summary>Tóm tắt giỏ hàng (chỉ đọc) — do Controller nạp, không nhận từ form.</summary>
    public IReadOnlyList<CartItem> Items { get; set; } = Array.Empty<CartItem>();

    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);

    public int TotalQuantity => Items.Sum(item => item.Quantity);
}
