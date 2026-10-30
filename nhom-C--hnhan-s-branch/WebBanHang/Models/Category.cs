namespace WebBanHang.Models;

/// <summary>Danh mục quần áo (bảng Categories).</summary>
public class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; }
}
