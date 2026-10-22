using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;

namespace WebBanHang.Controllers;

/// <summary>
/// Trang đặt hàng: hiển thị form thông tin nhận hàng + tóm tắt giỏ,
/// sau đó gọi IOrderRepository để ghi đơn trong một transaction.
/// </summary>
public class CheckoutController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderRepository _orderRepository;

    public CheckoutController(ICartService cartService, IOrderRepository orderRepository)
    {
        _cartService = cartService;
        _orderRepository = orderRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        IReadOnlyList<CartItem> items = _cartService.GetItems();

        if (items.Count == 0)
        {
            TempData["Error"] = "Giỏ hàng đang trống, hãy chọn sản phẩm trước khi đặt hàng.";
            return RedirectToAction(nameof(CartController.Index), "Cart");
        }

        return View(new CheckoutViewModel { Items = items });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(CheckoutViewModel model, CancellationToken cancellationToken = default)
    {
        IReadOnlyList<CartItem> items = _cartService.GetItems();

        if (items.Count == 0)
        {
            TempData["Error"] = "Giỏ hàng đang trống, hãy chọn sản phẩm trước khi đặt hàng.";
            return RedirectToAction(nameof(CartController.Index), "Cart");
        }

        if (!PaymentMethods.IsSupported(model.PaymentMethod))
        {
            ModelState.AddModelError(nameof(CheckoutViewModel.PaymentMethod),
                "Phương thức thanh toán không hợp lệ.");
        }

        if (!ModelState.IsValid)
        {
            model.Items = items;
            return View(model);
        }

        OrderResult result = await _orderRepository.CreateOrderAsync(new NewOrder
        {
            CustomerName = model.CustomerName.Trim(),
            PhoneNumber = model.PhoneNumber.Trim(),
            Email = string.IsNullOrWhiteSpace(model.Email) ? null : model.Email.Trim(),
            Address = model.Address.Trim(),
            Note = string.IsNullOrWhiteSpace(model.Note) ? null : model.Note.Trim(),
            PaymentMethod = model.PaymentMethod,
            Items = items
                .Select(item => new NewOrderItem
                {
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity
                })
                .ToList()
        }, cancellationToken);

        if (!result.Success)
        {
            // Giỏ vẫn giữ nguyên để khách sửa lại (giảm số lượng / chọn size khác)
            ModelState.AddModelError(string.Empty, result.ErrorMessage ?? "Đặt hàng không thành công.");
            model.Items = items;
            return View(model);
        }

        _cartService.Clear();

        TempData["OrderCode"] = result.OrderCode;
        TempData["OrderTotal"] = result.TotalAmount.ToString("N0") + " đ";
        TempData["OrderCustomer"] = model.CustomerName.Trim();
        TempData["OrderPhone"] = model.PhoneNumber.Trim();
        TempData["OrderAddress"] = model.Address.Trim();
        TempData["OrderPayment"] = model.PaymentMethod;

        return RedirectToAction(nameof(Success));
    }

    /// <summary>Trang xác nhận đặt hàng thành công (chỉ đọc dữ liệu từ TempData của chính phiên này).</summary>
    [HttpGet]
    public IActionResult Success()
    {
        if (TempData["OrderCode"] is not string orderCode)
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        var success = new OrderSuccessViewModel
        {
            OrderCode = orderCode,
            TotalText = TempData["OrderTotal"] as string ?? string.Empty,
            CustomerName = TempData["OrderCustomer"] as string ?? string.Empty,
            PhoneNumber = TempData["OrderPhone"] as string ?? string.Empty,
            Address = TempData["OrderAddress"] as string ?? string.Empty,
            PaymentMethod = TempData["OrderPayment"] as string ?? PaymentMethods.Cod
        };

        return View(success);
    }
}
