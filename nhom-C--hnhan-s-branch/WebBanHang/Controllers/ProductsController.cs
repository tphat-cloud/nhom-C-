using Microsoft.AspNetCore.Mvc;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Controllers;

public class ProductsController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Danh sách sản phẩm có tìm theo tên/mã và lọc theo danh mục.
    /// Ví dụ: /Products?keyword=ao&amp;categoryId=2
    /// </summary>
    public async Task<IActionResult> Index(
        string? keyword,
        int categoryId = 0,
        CancellationToken cancellationToken = default)
    {
        var viewModel = new ProductListViewModel
        {
            Keyword = keyword,
            CategoryId = categoryId,
            Categories = await _productRepository.GetActiveCategoriesAsync(cancellationToken),
            Products = await _productRepository.GetProductsAsync(keyword, categoryId, cancellationToken)
        };

        return View(viewModel);
    }

    /// <summary>Chi tiết sản phẩm kèm danh sách size/màu và tồn kho thực tế.</summary>
    public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken = default)
    {
        Product? product = await _productRepository.GetProductWithVariantsAsync(id, cancellationToken);

        if (product is null)
        {
            return NotFound();
        }

        return View(product);
    }
}
