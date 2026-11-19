using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Controllers;

public class HomeController : Controller
{
    /// <summary>
    /// Trang chủ: nạp danh mục + sản phẩm nổi bật trực tiếp từ SQL Server.
    /// </summary>
    public async Task<IActionResult> Index(
        [FromServices] IProductRepository productRepository,
        CancellationToken cancellationToken)
    {
        var viewModel = new HomeViewModel
        {
            Categories = await productRepository.GetActiveCategoriesAsync(cancellationToken),
            FeaturedProducts = await productRepository.GetFeaturedProductsAsync(8, cancellationToken),
            TotalProducts = await productRepository.CountActiveProductsAsync(cancellationToken)
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult StatusPage()
    {
        if (HttpContext.Features.Get<IStatusCodeReExecuteFeature>() is not null
            && Response.StatusCode != StatusCodes.Status404NotFound)
        {
            return new EmptyResult();
        }

        return new ViewResult
        {
            ViewName = "NotFound",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

