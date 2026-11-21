using Microsoft.AspNetCore.Mvc;
using Xunit;
using WebBanHang.Controllers;
using WebBanHang.Data;
using WebBanHang.Models;

namespace WebBanHang.Tests;

public sealed class ProductsControllerPaginationTests
{
    [Fact]
    public async Task Detail_LoadsUpToFourRelatedProductsForSameCategory()
    {
        var product = new Product { ProductId = 12, CategoryId = 3 };
        Product[] relatedProducts =
        {
            new() { ProductId = 21, CategoryId = 3 },
            new() { ProductId = 22, CategoryId = 3 }
        };
        var repository = new FakeProductRepository
        {
            DetailProduct = product,
            RelatedProducts = relatedProducts
        };
        var controller = new ProductsController(repository);

        var result = await controller.Detail(product.ProductId);

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Product>(view.Model);
        Assert.Equal(3, repository.RequestedRelatedCategoryId);
        Assert.Equal(12, repository.RequestedExcludedProductId);
        Assert.Equal(4, repository.RequestedRelatedTake);
        Assert.Equal(relatedProducts, model.RelatedProducts);
    }

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

    [Theory]
    [InlineData(ProductSortOptions.Featured)]
    [InlineData(ProductSortOptions.Newest)]
    [InlineData(ProductSortOptions.PriceAscending)]
    [InlineData(ProductSortOptions.PriceDescending)]
    public async Task Index_RecognizedSort_PassesSortAndFiltersToRepository(string sort)
    {
        var repository = new FakeProductRepository { TotalCount = 25 };
        var controller = new ProductsController(repository);

        ProductListViewModel model = await GetModelAsync(
            controller,
            keyword: "ao",
            categoryId: 3,
            page: 2,
            pageSize: 10,
            sort: sort);

        Assert.Equal(sort, model.Sort);
        Assert.Equal(sort, repository.RequestedSort);
        Assert.Equal("ao", repository.RequestedKeyword);
        Assert.Equal(3, repository.RequestedCategoryId);
        Assert.Equal(2, repository.RequestedPage);
        Assert.Equal(10, repository.RequestedPageSize);
    }

    [Fact]
    public async Task Index_UnsupportedSort_FallsBackToFeatured()
    {
        var repository = new FakeProductRepository { TotalCount = 10 };
        var controller = new ProductsController(repository);

        ProductListViewModel model = await GetModelAsync(
            controller,
            keyword: null,
            categoryId: 0,
            page: 1,
            pageSize: 10,
            sort: "price; DROP TABLE Products");

        Assert.Equal(ProductSortOptions.Featured, model.Sort);
        Assert.Equal(ProductSortOptions.Featured, repository.RequestedSort);
    }

    private static async Task<ProductListViewModel> GetModelAsync(
        ProductsController controller,
        string? keyword,
        int categoryId,
        int page,
        int pageSize,
        string? sort = ProductSortOptions.Featured)
    {
        var result = await controller.Index(keyword, categoryId, page, pageSize, sort);
        var viewResult = Assert.IsType<ViewResult>(result);
        return Assert.IsType<ProductListViewModel>(viewResult.ViewData!.Model);
    }

    private sealed class FakeProductRepository : IProductRepository
    {
        public int TotalCount { get; init; }
        public IReadOnlyList<Product> Products { get; init; } = Array.Empty<Product>();
        public Product? DetailProduct { get; init; }
        public IReadOnlyList<Product> RelatedProducts { get; init; } = Array.Empty<Product>();

        public string? RequestedKeyword { get; private set; }
        public int RequestedCategoryId { get; private set; }
        public int RequestedPage { get; private set; }
        public int RequestedPageSize { get; private set; }
        public string? RequestedSort { get; private set; }
        public int? RequestedRelatedCategoryId { get; private set; }
        public int? RequestedExcludedProductId { get; private set; }
        public int RequestedRelatedTake { get; private set; }

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
            string sort = ProductSortOptions.Featured,
            CancellationToken cancellationToken = default)
        {
            RequestedKeyword = keyword;
            RequestedCategoryId = categoryId;
            RequestedPage = page;
            RequestedPageSize = pageSize;
            RequestedSort = sort;
            return Task.FromResult(Products);
        }

        public Task<IReadOnlyList<Product>> GetRelatedProductsAsync(
            int categoryId,
            int excludedProductId,
            int take = 4,
            CancellationToken cancellationToken = default)
        {
            RequestedRelatedCategoryId = categoryId;
            RequestedExcludedProductId = excludedProductId;
            RequestedRelatedTake = take;
            return Task.FromResult(RelatedProducts);
        }

        public Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<Product?>(null);

        public Task<IReadOnlyList<ProductVariant>> GetVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ProductVariant>>(Array.Empty<ProductVariant>());

        public Task<VariantInfo?> GetVariantInfoAsync(int productVariantId, CancellationToken cancellationToken = default)
            => Task.FromResult<VariantInfo?>(null);

        public Task<Product?> GetProductWithVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult(DetailProduct);
    }
}
