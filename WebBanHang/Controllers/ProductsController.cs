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
        int page = 1,
        int pageSize = ProductListViewModel.DefaultPageSize,
        CancellationToken cancellationToken = default)
    {
        pageSize = pageSize < 1
            ? ProductListViewModel.DefaultPageSize
            : Math.Min(pageSize, ProductListViewModel.MaximumPageSize);

        int totalCount = await _productRepository.CountProductsAsync(keyword, categoryId, cancellationToken);
        int totalPages = totalCount == 0
            ? 0
            : (int)(((long)totalCount + pageSize - 1) / pageSize);
        page = Math.Clamp(page, 1, Math.Max(totalPages, 1));

        var viewModel = new ProductListViewModel
        {
            Keyword = keyword,
            CategoryId = categoryId,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            Categories = await _productRepository.GetActiveCategoriesAsync(cancellationToken),
            Products = await _productRepository.GetProductsAsync(
                keyword,
                categoryId,
                page,
                pageSize,
                cancellationToken)
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
