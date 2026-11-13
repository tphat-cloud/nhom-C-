using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Controllers;

/// <summary>
/// Controller phục vụ khách hàng tra cứu trạng thái và tiến trình đơn hàng
/// bằng Mã đơn hàng (OrderCode) + Số điện thoại (PhoneNumber).
/// </summary>
public class OrderLookupController : Controller
{
    private const int MaxLookupInputLength = 50;

    private readonly IOrderRepository _orderRepository;

    public OrderLookupController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet]
    [Route("tra-cuu-don-hang")]
    [Route("tra-cuu")]
    [Route("OrderLookup")]
    [Route("OrderLookup/Index")]
    public async Task<IActionResult> Index(
        [FromQuery] string? orderCode,
        [FromQuery] string? phoneNumber,
        CancellationToken cancellationToken = default)
    {
        string cleanOrderCode = orderCode?.Trim() ?? string.Empty;
        string cleanPhoneNumber = phoneNumber?.Trim() ?? string.Empty;

        var model = new OrderLookupViewModel
        {
            OrderCodeInput = cleanOrderCode,
            PhoneNumberInput = cleanPhoneNumber
        };

        // Khi khách vừa truy cập trang lần đầu, chưa nhập gì
        if (string.IsNullOrWhiteSpace(cleanOrderCode) && string.IsNullOrWhiteSpace(cleanPhoneNumber))
        {
            model.HasSearched = false;
            return View(model);
        }

        model.HasSearched = true;

        if (string.IsNullOrWhiteSpace(cleanOrderCode))
        {
            model.ErrorMessage = "Vui lòng nhập mã đơn hàng (ví dụ: DH26...).";
            return View(model);
        }

        if (string.IsNullOrWhiteSpace(cleanPhoneNumber))
        {
            model.ErrorMessage = "Vui lòng nhập số điện thoại đặt hàng để xác minh đơn.";
            return View(model);
        }

        if (cleanOrderCode.Length > MaxLookupInputLength || cleanPhoneNumber.Length > MaxLookupInputLength)
        {
            model.ErrorMessage = $"Mã đơn hàng và số điện thoại không được dài quá {MaxLookupInputLength} ký tự.";
            return View(model);
        }

        OrderLookupViewModel? order = await _orderRepository.FindOrderForLookupAsync(
            cleanOrderCode,
            cleanPhoneNumber,
            cancellationToken);

        if (order == null)
        {
            model.IsFound = false;
            model.ErrorMessage = "Không tìm thấy đơn hàng phù hợp. Vui lòng kiểm tra lại chính xác Mã đơn hàng và Số điện thoại bạn đã dùng khi đặt.";
            return View(model);
        }

        order.OrderCodeInput = cleanOrderCode;
        order.PhoneNumberInput = cleanPhoneNumber;
        return View(order);
    }
}
