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
        var model = new OrderLookupViewModel
        {
            OrderCodeInput = orderCode?.Trim(),
            PhoneNumberInput = phoneNumber?.Trim()
        };

        // Khi khách vừa truy cập trang lần đầu, chưa nhập gì
        if (string.IsNullOrWhiteSpace(orderCode) && string.IsNullOrWhiteSpace(phoneNumber))
        {
            model.HasSearched = false;
            return View(model);
        }

        model.HasSearched = true;

        if (string.IsNullOrWhiteSpace(orderCode))
        {
            model.ErrorMessage = "Vui lòng nhập mã đơn hàng (ví dụ: DH26...).";
            return View(model);
        }

        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            model.ErrorMessage = "Vui lòng nhập số điện thoại đặt hàng để xác minh đơn.";
            return View(model);
        }

        OrderLookupViewModel? order = await _orderRepository.FindOrderForLookupAsync(
            orderCode,
            phoneNumber,
            cancellationToken);

        if (order == null)
        {
            model.IsFound = false;
            model.ErrorMessage = "Không tìm thấy đơn hàng phù hợp. Vui lòng kiểm tra lại chính xác Mã đơn hàng và Số điện thoại bạn đã dùng khi đặt.";
            return View(model);
        }

        order.OrderCodeInput = orderCode.Trim();
        order.PhoneNumberInput = phoneNumber.Trim();
        return View(order);
    }
}
