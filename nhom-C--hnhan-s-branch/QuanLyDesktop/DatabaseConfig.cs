using Microsoft.Data.SqlClient;

namespace QuanLyDesktop;

internal static class DatabaseConfig
{
    public static string ConnectionString
    {
        get
        {
            string? password =
                Environment.GetEnvironmentVariable("FASHIONSHOP_SQL_PASSWORD");

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(
                    "Chưa cấu hình biến môi trường FASHIONSHOP_SQL_PASSWORD.");
            }

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = "127.0.0.1,1433",
                InitialCatalog = "FashionShopDb",
                UserID = "sa",
                Password = password,
                Encrypt = true,
                TrustServerCertificate = true
            };

            return builder.ConnectionString;
        }
    }
}