using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Xunit;
using WebBanHang.Controllers;
using WebBanHang.Data;
using WebBanHang.Models;
using WebBanHang.Services;

namespace WebBanHang.Tests;

public sealed class Web02EdgeCaseTests
{
    [Fact]
    public async Task CartUpdate_QuantityAboveStock_KeepsCartUnchanged()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 1
        });
        var repository = new FakeProductRepository
        {
            Variant = new VariantInfo
            {
                ProductVariantId = 10,
                StockQuantity = 2
            }
        };
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Update(10, 3);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(CartController.Index), redirect.ActionName);
        Assert.Equal(1, cart.GetItems()[0].Quantity);
        Assert.Contains("không đủ số lượng", cart.TempError(controller));
    }

    [Fact]
    public async Task CartUpdate_ZeroQuantity_RemovesItem()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 2
        });
        var repository = new FakeProductRepository();
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Update(10, 0);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.Empty(cart.GetItems());
        Assert.False(repository.VariantWasRequested);
    }

    [Fact]
    public async Task CartAdd_ValidQuantity_AddsVariantFromRepository()
    {
        var cart = new FakeCartService();
        var repository = new FakeProductRepository
        {
            Variant = new VariantInfo
            {
                ProductVariantId = 10,
                ProductName = "Áo thun",
                Price = 150_000m,
                StockQuantity = 5
            }
        };
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Add(10, 2);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(CartController.Index), redirect.ActionName);
        Assert.True(repository.VariantWasRequested);
        Assert.Equal(1, cart.AddCalls);
        Assert.Equal(2, cart.AddedQuantity);
        Assert.Equal(10, cart.AddedItem!.ProductVariantId);
        Assert.Equal("Áo thun", cart.AddedItem.ProductName);
        Assert.Equal(150_000m, cart.AddedItem.UnitPrice);
    }

    [Fact]
    public async Task CartAdd_QuantityAboveMaximum_DoesNotRequestVariantOrChangeCart()
    {
        var cart = new FakeCartService();
        var repository = new FakeProductRepository();
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Add(10, ICartService.MaxQuantityPerItem + 1);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.False(repository.VariantWasRequested);
        Assert.Equal(0, cart.AddCalls);
        Assert.Empty(cart.GetItems());
        Assert.Contains("từ 1 đến 99", cart.TempError(controller));
    }

    [Fact]
    public async Task CartAdd_CombinedQuantityAboveMaximum_KeepsExistingQuantity()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = ICartService.MaxQuantityPerItem - 1
        });
        var repository = new FakeProductRepository
        {
            Variant = new VariantInfo
            {
                ProductVariantId = 10,
                StockQuantity = ICartService.MaxQuantityPerItem
            }
        };
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Add(10, 2);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.Single(cart.GetItems());
        Assert.Equal(ICartService.MaxQuantityPerItem - 1, cart.GetItems()[0].Quantity);
        Assert.Equal(0, cart.AddCalls);
        Assert.Contains("tối đa 99", cart.TempError(controller));
    }

    [Fact]
    public async Task CartAdd_CombinedQuantityAboveStock_KeepsExistingQuantity()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 2
        });
        var repository = new FakeProductRepository
        {
            Variant = new VariantInfo
            {
                ProductVariantId = 10,
                StockQuantity = 3
            }
        };
        var controller = CreateCartController(cart, repository);

        IActionResult result = await controller.Add(10, 2);

        Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(2, cart.GetItems()[0].Quantity);
        Assert.Equal(0, cart.AddCalls);
        Assert.Contains("không đủ số lượng", cart.TempError(controller));
    }

    [Fact]
    public async Task Checkout_InvalidCartQuantity_DoesNotCallOrderRepository()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 0
        });
        var orderRepository = new FakeOrderRepository();
        var controller = CreateCheckoutController(cart, orderRepository);
        var model = ValidCheckoutModel();

        IActionResult result = await controller.Index(model);

        var view = Assert.IsType<ViewResult>(result);
        Assert.False(controller.ModelState.IsValid);
        Assert.False(orderRepository.CreateOrderWasCalled);
        Assert.Single(Assert.IsType<CheckoutViewModel>(view.Model).Items);
    }

    [Fact]
    public async Task Checkout_MissingRequiredInformation_DoesNotCallOrderRepository()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 1
        });
        var orderRepository = new FakeOrderRepository();
        var controller = CreateCheckoutController(cart, orderRepository);
        var model = ValidCheckoutModel();
        model.CustomerName = " ";

        IActionResult result = await controller.Index(model);

        var view = Assert.IsType<ViewResult>(result);
        Assert.False(controller.ModelState.IsValid);
        Assert.False(orderRepository.CreateOrderWasCalled);
        Assert.Single(Assert.IsType<CheckoutViewModel>(view.Model).Items);
    }

    [Fact]
    public async Task Checkout_InsufficientStock_ReturnsErrorAndPreservesCart()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 2
        });
        var orderRepository = new FakeOrderRepository
        {
            Result = OrderResult.Fail("Sản phẩm không đủ tồn kho.")
        };
        var controller = CreateCheckoutController(cart, orderRepository);

        IActionResult result = await controller.Index(ValidCheckoutModel());

        var view = Assert.IsType<ViewResult>(result);
        Assert.True(orderRepository.CreateOrderWasCalled);
        Assert.Contains(
            controller.ModelState.Values.SelectMany(entry => entry.Errors),
            error => error.ErrorMessage == "Sản phẩm không đủ tồn kho.");
        Assert.Single(Assert.IsType<CheckoutViewModel>(view.Model).Items);
        Assert.Single(cart.GetItems());
    }

    [Fact]
    public async Task Checkout_ValidModel_PassesTrimmedValuesToRepository()
    {
        var cart = new FakeCartService(new CartItem
        {
            ProductVariantId = 10,
            Quantity = 2,
            UnitPrice = 100_000m
        });
        var orderRepository = new FakeOrderRepository
        {
            Result = new OrderResult
            {
                Success = true,
                OrderCode = "DH2601-0001",
                TotalAmount = 200_000m
            }
        };
        var controller = CreateCheckoutController(cart, orderRepository);
        var model = ValidCheckoutModel();
        model.CustomerName = "  Nguyễn Văn An  ";
        model.PhoneNumber = " 0901234567 ";
        model.Address = "  12 Đường ABC  ";
        model.Email = "  khach@email.com  ";

        IActionResult result = await controller.Index(model);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(CheckoutController.Success), redirect.ActionName);
        Assert.True(orderRepository.CreateOrderWasCalled);
        Assert.Equal("Nguyễn Văn An", orderRepository.RequestedOrder!.CustomerName);
        Assert.Equal("0901234567", orderRepository.RequestedOrder.PhoneNumber);
        Assert.Equal("12 Đường ABC", orderRepository.RequestedOrder.Address);
        Assert.Equal("khach@email.com", orderRepository.RequestedOrder.Email);
    }

    [Fact]
    public async Task Checkout_EmptyCart_RedirectsToCartWithFriendlyError()
    {
        var cart = new FakeCartService();
        var orderRepository = new FakeOrderRepository();
        var controller = CreateCheckoutController(cart, orderRepository);

        IActionResult result = await controller.Index(ValidCheckoutModel());

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(CartController.Index), redirect.ActionName);
        Assert.Equal("Cart", redirect.ControllerName);
        Assert.False(orderRepository.CreateOrderWasCalled);
    }

    [Fact]
    public async Task OrderLookup_PhoneContainingLetters_ReturnsFriendlyError()
    {
        var repository = new FakeOrderRepository();
        var controller = CreateLookupController(repository);

        IActionResult result = await controller.Index("DH2601-0001", "090x123456");

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<OrderLookupViewModel>(view.Model);
        Assert.True(model.HasSearched);
        Assert.False(model.IsFound);
        Assert.Contains("chỉ được chứa chữ số", model.ErrorMessage);
        Assert.False(repository.LookupWasCalled);
    }

    [Fact]
    public async Task OrderLookup_MissingPhone_DoesNotCallRepository()
    {
        var repository = new FakeOrderRepository();
        var controller = CreateLookupController(repository);

        IActionResult result = await controller.Index("DH2601-0001", " ");

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<OrderLookupViewModel>(view.Model);
        Assert.Contains("Vui lòng nhập số điện thoại", model.ErrorMessage);
        Assert.False(repository.LookupWasCalled);
    }

    [Fact]
    public async Task OrderLookup_OrderNotFound_ReturnsFriendlyError()
    {
        var repository = new FakeOrderRepository();
        var controller = CreateLookupController(repository);

        IActionResult result = await controller.Index("DH2601-0001", "0901234567");

        var view = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<OrderLookupViewModel>(view.Model);
        Assert.True(repository.LookupWasCalled);
        Assert.True(model.HasSearched);
        Assert.False(model.IsFound);
        Assert.Contains("Không tìm thấy đơn hàng", model.ErrorMessage);
    }

    private static CheckoutViewModel ValidCheckoutModel() => new()
    {
        CustomerName = "Nguyễn Văn An",
        PhoneNumber = "0901234567",
        Address = "12 Đường ABC",
        PaymentMethod = PaymentMethods.Cod
    };

    private static CartController CreateCartController(FakeCartService cart, FakeProductRepository repository)
    {
        var controller = new CartController(cart, repository);
        AttachControllerContext(controller);
        return controller;
    }

    private static CheckoutController CreateCheckoutController(FakeCartService cart, FakeOrderRepository repository)
    {
        var controller = new CheckoutController(cart, repository);
        AttachControllerContext(controller);
        return controller;
    }

    private static OrderLookupController CreateLookupController(FakeOrderRepository repository)
    {
        var controller = new OrderLookupController(repository);
        AttachControllerContext(controller);
        return controller;
    }

    private static void AttachControllerContext(Controller controller)
    {
        var httpContext = new DefaultHttpContext();
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };
        controller.TempData = new TempDataDictionary(httpContext, new EmptyTempDataProvider());
    }

    private sealed class EmptyTempDataProvider : ITempDataProvider
    {
        public IDictionary<string, object> LoadTempData(HttpContext context)
            => new Dictionary<string, object>();

        public void SaveTempData(HttpContext context, IDictionary<string, object> values)
        {
        }
    }

    private sealed class FakeCartService : ICartService
    {
        private readonly List<CartItem> _items = new();

        public int AddCalls { get; private set; }
        public CartItem? AddedItem { get; private set; }
        public int AddedQuantity { get; private set; }

        public FakeCartService(params CartItem[] items)
        {
            _items.AddRange(items);
        }

        public IReadOnlyList<CartItem> GetItems() => _items;
        public int TotalQuantity => _items.Sum(item => item.Quantity);
        public decimal TotalAmount => _items.Sum(item => item.TotalPrice);

        public void Add(CartItem item, int quantity)
        {
            AddCalls++;
            AddedItem = item;
            AddedQuantity = quantity;
            _items.Add(item);
        }

        public void UpdateQuantity(int productVariantId, int quantity)
        {
            CartItem? item = _items.FirstOrDefault(x => x.ProductVariantId == productVariantId);
            if (item is null) return;
            if (quantity <= 0) _items.Remove(item);
            else item.Quantity = quantity;
        }

        public void Remove(int productVariantId)
            => _items.RemoveAll(item => item.ProductVariantId == productVariantId);

        public void Clear() => _items.Clear();

        public string TempError(Controller controller)
            => controller.TempData["Error"]?.ToString() ?? string.Empty;
    }

    private sealed class FakeProductRepository : IProductRepository
    {
        public VariantInfo? Variant { get; init; }
        public bool VariantWasRequested { get; private set; }

        public Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Category>>(Array.Empty<Category>());

        public Task<int> CountActiveProductsAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(0);

        public Task<IReadOnlyList<Product>> GetFeaturedProductsAsync(int take = 8, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Product>>(Array.Empty<Product>());

        public Task<int> CountProductsAsync(string? keyword, int categoryId, CancellationToken cancellationToken = default)
            => Task.FromResult(0);

        public Task<IReadOnlyList<Product>> GetProductsAsync(
            string? keyword,
            int categoryId,
            int page = 1,
            int pageSize = ProductListViewModel.DefaultPageSize,
            string sort = ProductSortOptions.Featured,
            CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Product>>(Array.Empty<Product>());

        public Task<IReadOnlyList<Product>> GetRelatedProductsAsync(
            int categoryId,
            int excludedProductId,
            int take = 4,
            CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<Product>>(Array.Empty<Product>());

        public Task<Product?> GetProductByIdAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<Product?>(null);

        public Task<IReadOnlyList<ProductVariant>> GetVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<IReadOnlyList<ProductVariant>>(Array.Empty<ProductVariant>());

        public Task<VariantInfo?> GetVariantInfoAsync(int productVariantId, CancellationToken cancellationToken = default)
        {
            VariantWasRequested = true;
            return Task.FromResult(Variant);
        }

        public Task<Product?> GetProductWithVariantsAsync(int productId, CancellationToken cancellationToken = default)
            => Task.FromResult<Product?>(null);
    }

    private sealed class FakeOrderRepository : IOrderRepository
    {
        public bool CreateOrderWasCalled { get; private set; }
        public bool LookupWasCalled { get; private set; }
        public NewOrder? RequestedOrder { get; private set; }
        public OrderResult Result { get; init; } = new();

        public Task<OrderResult> CreateOrderAsync(NewOrder order, CancellationToken cancellationToken = default)
        {
            CreateOrderWasCalled = true;
            RequestedOrder = order;
            return Task.FromResult(Result);
        }

        public Task<OrderLookupViewModel?> FindOrderForLookupAsync(
            string orderCode,
            string phoneNumber,
            CancellationToken cancellationToken = default)
        {
            LookupWasCalled = true;
            return Task.FromResult<OrderLookupViewModel?>(null);
        }
    }
}
