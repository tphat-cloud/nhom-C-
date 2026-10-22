using System.Text.Json;
using WebBanHang.Models;

namespace WebBanHang.Services;

/// <summary>
/// Giỏ hàng lưu dưới dạng JSON trong Session (mỗi khách một giỏ, tự hết hạn theo Session).
/// Giá trong giỏ chỉ để hiển thị — khi đặt hàng, SqlOrderRepository đọc lại giá từ CSDL.
/// </summary>
public sealed class CartService : ICartService
{
    private const string SessionKey = "FashionShop.Cart";

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession? Session => _httpContextAccessor.HttpContext?.Session;

    public int TotalQuantity => GetItems().Sum(item => item.Quantity);

    public decimal TotalAmount => GetItems().Sum(item => item.TotalPrice);

    public IReadOnlyList<CartItem> GetItems()
    {
        ISession? session = Session;
        if (session is null)
        {
            return Array.Empty<CartItem>();
        }

        byte[]? data = session.Get(SessionKey);
        if (data is null || data.Length == 0)
        {
            return Array.Empty<CartItem>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<CartItem>>(data, JsonOptions) ?? new List<CartItem>();
        }
        catch (JsonException)
        {
            // Session bị hỏng (đổi định dạng model...) thì bỏ giỏ cũ thay vì lỗi 500
            session.Remove(SessionKey);
            return Array.Empty<CartItem>();
        }
    }

    public void Add(CartItem item, int quantity)
    {
        if (quantity <= 0)
        {
            return;
        }

        List<CartItem> items = GetItems().ToList();
        CartItem? existing = items.FirstOrDefault(x => x.ProductVariantId == item.ProductVariantId);

        if (existing is null)
        {
            item.Quantity = Math.Min(quantity, ICartService.MaxQuantityPerItem);
            items.Add(item);
        }
        else
        {
            existing.Quantity = Math.Min(existing.Quantity + quantity, ICartService.MaxQuantityPerItem);
            // Làm mới thông tin hiển thị theo dữ liệu CSDL mới nhất
            existing.ProductName = item.ProductName;
            existing.UnitPrice = item.UnitPrice;
            existing.ImageUrl = item.ImageUrl;
        }

        Save(items);
    }

    public void UpdateQuantity(int productVariantId, int quantity)
    {
        List<CartItem> items = GetItems().ToList();
        CartItem? existing = items.FirstOrDefault(x => x.ProductVariantId == productVariantId);
        if (existing is null)
        {
            return;
        }

        if (quantity <= 0)
        {
            items.Remove(existing);
        }
        else
        {
            existing.Quantity = Math.Min(quantity, ICartService.MaxQuantityPerItem);
        }

        Save(items);
    }

    public void Remove(int productVariantId)
    {
        List<CartItem> items = GetItems().ToList();
        int removed = items.RemoveAll(x => x.ProductVariantId == productVariantId);
        if (removed > 0)
        {
            Save(items);
        }
    }

    public void Clear() => Session?.Remove(SessionKey);

    private void Save(List<CartItem> items)
    {
        ISession? session = Session;
        if (session is null)
        {
            return;
        }

        if (items.Count == 0)
        {
            session.Remove(SessionKey);
            return;
        }

        session.Set(SessionKey, JsonSerializer.SerializeToUtf8Bytes(items, JsonOptions));
    }
}
