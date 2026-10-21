using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Controllers;

public class HomeController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IProductRepository productRepository, ILogger<HomeController> logger)
    {
        _productRepository = productRepository;
        _logger = logger;
    }

    /// <summary>
    /// Trang chủ: nạp danh mục + sản phẩm nổi bật trực tiếp từ SQL Server.
    /// </summary>
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var viewModel = new HomeViewModel
        {
            Categories = await _productRepository.GetActiveCategoriesAsync(cancellationToken),
            FeaturedProducts = await _productRepository.GetFeaturedProductsAsync(8, cancellationToken),
            TotalProducts = await _productRepository.CountActiveProductsAsync(cancellationToken)
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

