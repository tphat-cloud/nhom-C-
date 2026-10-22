using System.Globalization;
using Microsoft.AspNetCore.Localization;
using WebBanHang.Data;
using WebBanHang.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Giỏ hàng lưu trong Session -> cần distributed cache (in-memory) + HttpContextAccessor
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".FashionShop.Session";
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICartService, CartService>();

// Tầng dữ liệu: ADO.NET thuần (Microsoft.Data.SqlClient) - đồng bộ với QuanLyDesktop
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();
builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();

var app = builder.Build();

// Cảnh báo sớm nếu thiếu mật khẩu CSDL, thay vì để khách nhận lỗi 500 khó hiểu
if (!SqlConnectionFactory.IsPasswordConfigured(app.Configuration))
{
    app.Logger.LogWarning(
        "Chưa cấu hình biến môi trường {Variable}. Web sẽ không truy vấn được SQL Server. " +
        "Chạy: setx {Variable} \"mat-khau-sa\" rồi mở lại terminal/Visual Studio.",
        SqlConnectionFactory.PasswordEnvironmentVariable,
        SqlConnectionFactory.PasswordEnvironmentVariable);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Định dạng tiền tệ Việt Nam: 345.000 đ
var vietnameseCulture = new CultureInfo("vi-VN");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(vietnameseCulture),
    SupportedCultures = new[] { vietnameseCulture },
    SupportedUICultures = new[] { vietnameseCulture }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session phải nằm sau UseRouting và trước MapControllerRoute
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

