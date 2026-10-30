using Microsoft.Data.SqlClient;

namespace WebBanHang.Data;

/// <summary>Tạo kết nối SQL Server cho tầng dữ liệu.</summary>
public interface ISqlConnectionFactory
{
    SqlConnection CreateConnection();
}

/// <summary>
/// Đọc cấu hình kết nối từ mục "Database" trong appsettings.json,
/// còn mật khẩu SA lấy từ biến môi trường FASHIONSHOP_SQL_PASSWORD
/// (giống QuanLyDesktop/DatabaseConfig.cs) để KHÔNG lưu mật khẩu trong mã nguồn.
/// </summary>
public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    public const string PasswordEnvironmentVariable = "FASHIONSHOP_SQL_PASSWORD";

    private const string DefaultServer = "127.0.0.1,1433";
    private const string DefaultDatabase = "FashionShopDb";
    private const string DefaultUserId = "sa";

    private readonly string _connectionString;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        string? password = configuration[PasswordEnvironmentVariable];

        if (string.IsNullOrWhiteSpace(password))
        {
            password = Environment.GetEnvironmentVariable(PasswordEnvironmentVariable);
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new InvalidOperationException(
                $"Chưa cấu hình biến môi trường {PasswordEnvironmentVariable}. " +
                $"Chạy lệnh: setx {PasswordEnvironmentVariable} \"mat-khau-sa\" rồi mở lại terminal/Visual Studio.");
        }

        IConfigurationSection database = configuration.GetSection("Database");

        var builder = new SqlConnectionStringBuilder
        {
            DataSource = database["Server"] ?? DefaultServer,
            InitialCatalog = database["Name"] ?? DefaultDatabase,
            UserID = database["UserId"] ?? DefaultUserId,
            Password = password,
            Encrypt = true,
            TrustServerCertificate = true,
            ConnectTimeout = 15,
            ApplicationName = "WebBanHang"
        };

        _connectionString = builder.ConnectionString;
    }

    public SqlConnection CreateConnection() => new(_connectionString);

    /// <summary>
    /// Kiểm tra nhanh lúc khởi động để ghi cảnh báo rõ ràng thay vì lỗi 500 khó hiểu.
    /// </summary>
    public static bool IsPasswordConfigured(IConfiguration configuration)
    {
        return !string.IsNullOrWhiteSpace(configuration[PasswordEnvironmentVariable])
            || !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable(PasswordEnvironmentVariable));
    }
}
