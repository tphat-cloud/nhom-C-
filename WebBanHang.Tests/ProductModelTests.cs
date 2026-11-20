using WebBanHang.Models;
using Xunit;

namespace WebBanHang.Tests;

public sealed class ProductModelTests
{
    private const string ProductImage = "/images/product.jpg";
    private const string CategoryImage = "/images/category.jpg";
    private const string DefaultImage = "/images/no-image.svg";

    [Theory]
    [InlineData(150_000, 200_000, 25)]
    [InlineData(100_000, 300_000, 67)]
    [InlineData(200_000, 300_000, 33)]
    [InlineData(150_000, null, 0)]
    [InlineData(150_000, 0, 0)]
    [InlineData(150_000, 150_000, 0)]
    [InlineData(150_000, 100_000, 0)]
    public void DiscountPercent_RoundsOnlyValidDiscounts(int price, int? originalPrice, int expectedPercent)
    {
        var product = new Product { Price = price, OriginalPrice = originalPrice };

        Assert.Equal(expectedPercent, product.DiscountPercent);
    }

    [Theory]
    [InlineData(0, "Hết hàng")]
    [InlineData(1, "Sắp hết hàng")]
    [InlineData(5, "Sắp hết hàng")]
    [InlineData(6, "Còn hàng")]
    public void StockText_ReportsAvailabilityAtStockThresholds(int totalStock, string expectedText)
    {
        var product = new Product { TotalStock = totalStock };

        Assert.Equal(expectedText, product.StockText);
    }

    [Theory]
    [InlineData(0, false)]
    [InlineData(1, true)]
    public void IsInStock_WithoutVariants_UsesTotalStock(int totalStock, bool expectedInStock)
    {
        var product = new Product { TotalStock = totalStock };

        Assert.Equal(expectedInStock, product.IsInStock);
    }

    [Theory]
    [InlineData(4, 0, false)]
    [InlineData(0, 1, true)]
    public void IsInStock_WithVariants_ChecksWhetherAnyVariantHasStock(
        int totalStock, int secondVariantStock, bool expectedInStock)
    {
        var product = new Product
        {
            TotalStock = totalStock,
            Variants = new[]
            {
                new ProductVariant { StockQuantity = 0 },
                new ProductVariant { StockQuantity = secondVariantStock }
            }
        };

        Assert.Equal(expectedInStock, product.IsInStock);
    }

    [Theory]
    [InlineData(ProductImage, CategoryImage, ProductImage, CategoryImage)]
    [InlineData(ProductImage, null, ProductImage, DefaultImage)]
    [InlineData(null, CategoryImage, CategoryImage, CategoryImage)]
    [InlineData("   ", CategoryImage, CategoryImage, CategoryImage)]
    [InlineData(null, null, DefaultImage, DefaultImage)]
    [InlineData("", "   ", DefaultImage, DefaultImage)]
    public void ProductImages_UseCategoryAndDefaultImagesAsFallbacks(
        string? imageUrl, string? categoryImageUrl, string expectedDisplayImage, string expectedFallbackImage)
    {
        var product = new Product { ImageUrl = imageUrl, CategoryImageUrl = categoryImageUrl };

        Assert.Equal(expectedDisplayImage, product.DisplayImage);
        Assert.Equal(expectedFallbackImage, product.FallbackImage);
    }

    [Theory]
    [InlineData(0, 12, 1, 0, false)]
    [InlineData(12, 12, 1, 1, false)]
    [InlineData(13, 12, 1, 2, true)]
    [InlineData(24, 12, 2, 2, false)]
    [InlineData(25, 12, 3, 3, false)]
    [InlineData(int.MaxValue, 12, 1, 178_956_971, true)]
    public void Pagination_CountsPartialPagesAndDetectsWhenNextPageExists(
        int totalCount, int pageSize, int page, int expectedTotalPages, bool expectedHasNextPage)
    {
        var model = new ProductListViewModel { TotalCount = totalCount, PageSize = pageSize, Page = page };

        Assert.Equal(expectedTotalPages, model.TotalPages);
        Assert.Equal(expectedHasNextPage, model.HasNextPage);
    }
}
