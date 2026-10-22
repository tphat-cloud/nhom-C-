namespace WebBanHang.Models;

/// <summary>Kết quả tạo đơn hàng: thành công (kèm mã đơn) hoặc thất bại (kèm lý do thân thiện).</summary>
public sealed class OrderResult
{
    public bool Success { get; init; }

    public string OrderCode { get; init; } = string.Empty;

    public decimal TotalAmount { get; init; }

    public string? ErrorMessage { get; init; }

    public static OrderResult Ok(string orderCode, decimal totalAmount) => new()
    {
        Success = true,
        OrderCode = orderCode,
        TotalAmount = totalAmount
    };

    public static OrderResult Fail(string errorMessage) => new()
    {
        Success = false,
        ErrorMessage = errorMessage
    };
}
