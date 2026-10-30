using System.Globalization;
using Microsoft.AspNetCore.Localization;
using WebBanHang.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Tầng dữ liệu: ADO.NET thuần (Microsoft.Data.SqlClient) - đồng bộ với QuanLyDesktop
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

