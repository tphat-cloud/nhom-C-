namespace WebBanHang.Models;

/// <summary>
/// Bảng màu dùng để vẽ ô swatch cho các giá trị <c>ProductVariants.Color</c>.
/// Màu lạ (cửa hàng nhập thêm) sẽ hiển thị ô trung tính thay vì đoán sai màu,
/// nên thêm màu mới chỉ cần bổ sung một dòng vào <see cref="Palette"/>.
/// </summary>
public static class ColorSwatch
{
    private static readonly Dictionary<string, string> Palette = new(StringComparer.OrdinalIgnoreCase)
    {
        ["Trắng"] = "#f6f4ef",
        ["Kem"] = "#f0e7d6",
        ["Be"] = "#e0d1ba",
        ["Đen"] = "#1b1a18",
        ["Xám"] = "#9c9a95",
        ["Nâu"] = "#7a5230",
        ["Đỏ"] = "#b2352f",
        ["Hồng"] = "#e6b0ba",
        ["Vàng"] = "#d8b969",
        ["Xanh"] = "#7d9dbb",
        ["Xanh đậm"] = "#263d5c",
        ["Xanh navy"] = "#1f3350",
        ["Xanh rêu"] = "#4c5c43"
    };

    /// <summary>Mã màu hex của tên màu, null nếu chưa định nghĩa.</summary>
    public static string? GetHex(string? colorName) =>
        !string.IsNullOrWhiteSpace(colorName) && Palette.TryGetValue(colorName.Trim(), out string? hex)
            ? hex
            : null;

    /// <summary>Màu sáng cần viền đậm hơn để không chìm vào nền trắng.</summary>
    public static bool IsLight(string? colorName) => GetHex(colorName) switch
    {
        null => false,
        var hex => hex is "#f6f4ef" or "#f0e7d6" or "#e0d1ba" or "#d8b969" or "#e6b0ba"
    };
}
