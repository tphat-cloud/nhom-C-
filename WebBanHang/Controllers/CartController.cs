using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;

namespace WebBanHang.Controllers;

/// <summary>
/// Giỏ hàng của khách (lưu trong Session, không cần đăng nhập).
/// Giá/tên sản phẩm luôn đọc lại từ CSDL khi thêm hàng — không tin tham số từ client.
/// </summary>
public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IProductRepository _productRepository;

    public CartController(ICartService cartService, IProductRepository productRepository)
    {
        _cartService = cartService;
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult Index() => View(_cartService.GetItems());

    /// <summary>Thêm một biến thể size/màu vào giỏ.</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(
        int variantId,
        int quantity = 1,
        string? returnUrl = null,
        CancellationToken cancellationToken = default)
    {
        quantity = Math.Clamp(quantity, 1, ICartService.MaxQuantityPerItem);

        VariantInfo? variant = await _productRepository.GetVariantInfoAsync(variantId, cancellationToken);

        if (variant is null)
        {
            TempData["Error"] = "Sản phẩm không tồn tại hoặc đã ngừng bán.";
            return RedirectBack(returnUrl);
        }

        if (!variant.IsAvailable)
        {
            TempData["Error"] =
                $"\"{variant.ProductName}\" (size {variant.Size}, màu {variant.Color}) đã hết hàng.";
            return RedirectBack(returnUrl);
        }

        int alreadyInCart = _cartService.GetItems()
            .FirstOrDefault(item => item.ProductVariantId == variantId)?.Quantity ?? 0;

        // Không tiết lộ con số tồn kho chính xác cho khách
        if (alreadyInCart + quantity > variant.StockQuantity)
        {
            TempData["Error"] =
                $"\"{variant.ProductName}\" (size {variant.Size}, màu {variant.Color}) không đủ số lượng bạn chọn " +
                "(có thể vừa được khách khác mua). Vui lòng chọn số lượng nhỏ hơn.";
            return RedirectBack(returnUrl);
        }

        _cartService.Add(variant.ToCartItem(quantity), quantity);

        TempData["Message"] =
            $"Đã thêm \"{variant.ProductName}\" (size {variant.Size}, màu {variant.Color}) vào giỏ hàng.";

        return RedirectBack(returnUrl);
    }

    /// <summary>Cập nhật số lượng một dòng trong giỏ (0 = xóa dòng).</summary>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int variantId, int quantity)
    {
        _cartService.UpdateQuantity(variantId, Math.Clamp(quantity, 0, ICartService.MaxQuantityPerItem));

        TempData["Message"] = quantity <= 0
            ? "Đã xóa sản phẩm khỏi giỏ hàng."
            : "Đã cập nhật số lượng trong giỏ hàng.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int variantId)
    {
        _cartService.Remove(variantId);
        TempData["Message"] = "Đã xóa sản phẩm khỏi giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _cartService.Clear();
        TempData["Message"] = "Đã xóa toàn bộ giỏ hàng.";
        return RedirectToAction(nameof(Index));
    }

    /// <summary>Quay lại trang vừa xem (chỉ chấp nhận URL nội bộ để tránh open redirect).</summary>
    private IActionResult RedirectBack(string? returnUrl)
        => !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? Redirect(returnUrl)
            : RedirectToAction(nameof(Index));
}
