using Microsoft.AspNetCore.Mvc;
using Xunit;
using WebBanHang.Controllers;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Tests;

public sealed class ProductsControllerPaginationTests
{
    [Fact]
    public async Task Index_InvalidPageAndPageSize_UsesSafeDefaults()
    {
        var repository = new FakeProductRepository { TotalCount = 25 };
        var controller = new ProductsController(repository);

        var model = await GetModelAsync(controller, keyword: null, categoryId: 0, page: 0, pageSize: 0);

        Assert.Equal(1, model.Page);
        Assert.Equal(ProductListViewModel.DefaultPageSize, model.PageSize);
        Assert.Equal(1, repository.RequestedPage);
        Assert.Equal(ProductListViewModel.DefaultPageSize, repository.RequestedPageSize);
    }

    [Fact]
    public async Task Index_PageSizeAboveMaximum_IsClampedBeforeQuery()
    {
        var repository = new FakeProductRepository { TotalCount = 60 };
        var controller = new ProductsController(repository);

        var model = await GetModelAsync(controller, keyword: null, categoryId: 0, page: 2, pageSize: 999);

        Assert.Equal(ProductListViewModel.MaximumPageSize, model.PageSize);
        Assert.Equal(2, model.Page);
        Assert.Equal(ProductListViewModel.MaximumPageSize, repository.RequestedPageSize);
    }

    [Fact]
    public async Task Index_PageOutsideRange_UsesLastPage()
    {
        var repository = new FakeProductRepository { TotalCount = 25 };
        var controller = new ProductsController(repository);

        var model = await GetModelAsync(controller, keyword: null, categoryId: 0, page: 99, pageSize: 10);

        Assert.Equal(3, model.TotalPages);
        Assert.Equal(3, model.Page);
        Assert.Equal(3, repository.RequestedPage);
    }

    [Fact]
    public async Task Index_WithoutMatchingProducts_ReturnsEmptyValidPage()
    {
        var repository = new FakeProductRepository { TotalCount = 0 };
        var controller = new ProductsController(repository);

        var model = await GetModelAsync(controller, keyword: "khong-co", categoryId: 7, page: 4, pageSize: 10);

        Assert.Empty(model.Products);
        Assert.Equal(0, model.TotalCount);
        Assert.Equal(0, model.TotalPages);
        Assert.Equal(1, model.Page);
        Assert.Equal(1, repository.RequestedPage);
        Assert.Equal(7, repository.RequestedCategoryId);
    }

    [Fact]
    public async Task Index_PassesKeywordCategoryAndPagingToRepository()
    {
        var expectedProducts = new[]
        {
            new Product { ProductId = 101, ProductCode = "AK-001", Name = "Áo khoác xanh" },
            new Product { ProductId = 102, ProductCode = "AK-002", Name = "Áo khoác đen" }
        };
        var repository = new FakeProductRepository
        {
            TotalCount = 31,
            Products = expectedProducts
        };
        var controller = new ProductsController(repository);

        var model = await GetModelAsync(controller, keyword: "ao khoac", categoryId: 3, page: 2, pageSize: 10);

        Assert.Equal(expectedProducts, model.Products);
        Assert.Equal("ao khoac", model.Keyword);
        Assert.Equal(3, model.CategoryId);
        Assert.Equal("ao khoac", repository.RequestedKeyword);
        Assert.Equal(3, repository.RequestedCategoryId);
        Assert.Equal(2, repository.RequestedPage);
        Assert.Equal(10, repository.RequestedPageSize);
    }

    private static async Task<ProductListViewModel> GetModelAsync(
        ProductsController controller,
        string? keyword,
        int categoryId,
        int page,
        int pageSize)
    {
        var result = await controller.Index(keyword, categoryId, page, pageSize);
        var viewResult = Assert.IsType<ViewResult>(result);
        return Assert.IsType<ProductListViewModel>(viewResult.ViewData!.Model);
    }

    private sealed class FakeProductRepository : IProductRepository
    {
        public int TotalCount { get; init; }
        public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();

        public string? RequestedKeyword { get; private set; }
        public int RequestedCategoryId { get; private set; }
        public int RequestedPage { get; private set; }
        public int RequestedPageSize { get; private set; }

        public Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Category>>(Array.Empty<Category>());

        public Task<int> CountActiveProductsAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(TotalCount);

        public Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int take = 8, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Product>>(Array.Empty<Product>());

        public Task<int> CountProductsAsync(string? keyword, int categoryId, CancellationToken cancellationToken = default)
        {
            RequestedKeyword = keyword;
            RequestedCategoryId = categoryId;
            return Task.FromResult(TotalCount);
        }

        public Task<IReadOnlyList<Product>> GetProductsAsync(
            string? keyword,
            int categoryId,
            int page = 1,
            int pageSize = ProductListViewModel.DefaultPageSize,
            CancellationToken cancellationToken = default)
        {
            RequestedKeyword = keyword;
            RequestedCategoryId = categoryId;
            RequestedPage = page;
            RequestedPageSize = pageSize;
            return Task.FromResult(Products);
        }

        public Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<Product?>(null);

        public Task<IReadOnlyList<ProductVariant>> GetVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ProductVariant>>(Array.Empty<ProductVariant>());

        public Task<VariantInfo?> GetVariantInfoAsync(int productVariantId, CancellationToken cancellationToken = default)
            => Task.FromResult<VariantInfo?>(null);

        public Task<Product?> GetProductWithVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<Product?>(null);
    }
}
