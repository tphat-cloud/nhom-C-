# Đồ án C# - Quản lý & Bán quần áo thời trang

> Môn: **Ngôn ngữ lập trình C#** — GVGD: TS. Đỗ Như Tài

Hệ thống gồm **2 ứng dụng dùng chung 1 cơ sở dữ liệu**:

| Thành phần | Công nghệ | Vai trò |
| --- | --- | --- |
| `QuanLyDesktop` | WinForms (.NET 8, ADO.NET) | Admin/nhân viên: quản lý sản phẩm, danh mục, size/màu, duyệt đơn, dashboard |
| `WebBanHang` | ASP.NET Core MVC (.NET 8) | Khách hàng: xem sản phẩm, giỏ hàng, đặt hàng |
| `mssql2022` | SQL Server 2022 trong Docker | Database `FashionShopDb` dùng chung cho cả 2 app |

```
WebBanHang (khách đặt hàng)  ──┐
                               ├──► SQL Server 2022 (Docker) ──► FashionShopDb
QuanLyDesktop (admin duyệt)  ──┘
```

---

## 1. Yêu cầu môi trường

- Docker Desktop đang chạy
- .NET SDK 8.0 trở lên (`dotnet --version`)
- Visual Studio 2022 17.13+ (nếu mở file `QuanLyShopQuanAo.slnx`)

---

## 2. Cài đặt lần đầu (máy mới clone repo)

### Bước 1 — Tạo file biến môi trường

```powershell
Copy-Item .env.example .env
# Mở .env và đổi MSSQL_SA_PASSWORD thành mật khẩu mạnh của bạn
```

> `.env` đã nằm trong `.gitignore` → **không bao giờ commit mật khẩu lên Git**.

### Bước 2 — Bật SQL Server bằng Docker Compose

```powershell
docker compose up -d
docker compose ps          # chờ STATUS = healthy (khoảng 20-30 giây)
```

### Bước 3 — Tạo database và nạp dữ liệu mẫu (chỉ lần đầu)

Script đã được mount sẵn vào container tại `/scripts/CSDL_ShopQuanAo.sql`:

```powershell
$p = ((Select-String -Path .env -Pattern '^MSSQL_SA_PASSWORD=').Line -split '=', 2)[1]
docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$p" -C -f 65001 -i /scripts/CSDL_ShopQuanAo.sql
```

> Luôn thêm `-f 65001` khi chạy file `.sql` có tiếng Việt, nếu thiếu sẽ bị lỗi font dữ liệu.

Kiểm tra lại:

```powershell
docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$p" -C -Q "SELECT name FROM sys.tables" -d FashionShopDb -W
```

### Bước 3b — Nâng cấp database đang có (không mất dữ liệu)

Nếu bạn đã có `FashionShopDb` từ phiên bản cũ (chưa có ràng buộc toàn vẹn, chưa có tồn kho), chạy migration:

```powershell
docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$p" -C -f 65001 -i /migrations/01_harden_database.sql
```

Migration `01_harden_database.sql` sẽ:
- Thêm **9 CHECK constraint** (giá > 0, tồn kho ≥ 0, trạng thái đơn hợp lệ, số lượng > 0...)
- Thêm **UNIQUE index** `(ProductId, Size, Color)` — không thể trùng biến thể
- Thêm **5 index** tra cứu (`Orders.Status`, `Orders.OrderDate`, `CustomerPhone`, `OrderDetails.OrderId`, `Products.CategoryId`)
- Tạo view `vw_ProductStock` (tổng tồn kho theo sản phẩm)
- Nạp **30 biến thể size/màu + tồn kho** mẫu (chỉ khi bảng còn trống)

Script **idempotent** — chạy lại nhiều lần không lỗi, không nhân đôi dữ liệu.

### Bước 4 — Cấu hình mật khẩu cho app Desktop & Web

Cả hai app đều **không hard-code mật khẩu**, mà đọc từ biến môi trường `FASHIONSHOP_SQL_PASSWORD`
(`QuanLyDesktop/DatabaseConfig.cs` và `WebBanHang/Data/SqlConnectionFactory.cs`):

```powershell
# Chỉ có hiệu lực trong cửa sổ PowerShell hiện tại
$env:FASHIONSHOP_SQL_PASSWORD = "mat-khau-sa-cua-ban"

# Hoặc lưu vĩnh viễn cho user hiện tại (cần mở lại Visual Studio / terminal)
setx FASHIONSHOP_SQL_PASSWORD "mat-khau-sa-cua-ban"
```

> ⚠️ Nếu mở Visual Studio **trước khi** `setx`, VS không thấy biến này → phải **tắt hẳn VS rồi mở lại**.
> Biến này phải **trùng** với `MSSQL_SA_PASSWORD` trong `.env`.

### Bước 5 — Chạy ứng dụng

```powershell
# Build toàn bộ solution
dotnet build QuanLyShopQuanAo.slnx

# App quản lý (WinForms)
dotnet run --project QuanLyDesktop

# Web bán hàng
dotnet run --project WebBanHang

# Web bán hàng — cách nhanh nhất: 1 lệnh, tự nạp mật khẩu từ .env,
# tự bật container SQL Server nếu chưa chạy, rồi mở http://localhost:5193
.\run-web.cmd
```

> 💡 Muốn khỏi phụ thuộc biến môi trường (dùng được cả nút **F5** trong VS Code/Visual Studio), lưu mật khẩu vào **User Secrets** một lần duy nhất:
>
> ```powershell
> dotnet user-secrets init   --project WebBanHang
> dotnet user-secrets set "FASHIONSHOP_SQL_PASSWORD" "mat-khau-sa-cua-ban" --project WebBanHang
> ```
>
> Sau đó chỉ cần `dotnet run --project WebBanHang` hoặc F5 — không cần `setx`, không cần `$env:` mỗi phiên.

Tài khoản mẫu (xem trong `CSDL_ShopQuanAo.sql`):

| Tên đăng nhập | Mật khẩu | Quyền |
| --- | --- | --- |
| `admin` | `123456` | Admin — toàn quyền |
| `nhanvien` | `123456` | Staff — không được thêm/sửa/xóa sản phẩm, danh mục |

---

## 3. Tầng dữ liệu Web (ASP.NET Core ↔ SQL Server)

Web dùng **ADO.NET thuần** (`Microsoft.Data.SqlClient` 5.2.2) giống app Desktop — **không dùng Entity Framework**, câu SQL tập trung trong lớp `Data/` chứ không rải trong Controller.

> 🛍️ **Khách chỉ thấy tình trạng hàng** (*Còn hàng / Sắp hết hàng / Hết hàng*), **không thấy số lượng tồn kho chính xác** — đó là dữ liệu nội bộ, chỉ hiển thị trong ứng dụng quản lý. Tồn kho vẫn được dùng ở tầng dữ liệu để chặn đặt quá số lượng khi làm chức năng đặt hàng.

| Thành phần | Vai trò |
| --- | --- |
| `WebBanHang/Data/SqlConnectionFactory.cs` | Tạo `SqlConnection`; đọc `Database:Server/Name/UserId` trong `appsettings.json`, mật khẩu lấy từ biến môi trường `FASHIONSHOP_SQL_PASSWORD` |
| `WebBanHang/Data/IProductRepository.cs` | Hợp đồng truy vấn sản phẩm / danh mục |
| `WebBanHang/Data/SqlProductRepository.cs` | Cài đặt bằng ADO.NET: tham số hóa 100%, `async` + `CancellationToken`, đọc dữ liệu an toàn khi cột `NULL` |
| `WebBanHang/Models/` | `Product`, `ProductVariant`, `Category`, `HomeViewModel`, `ProductListViewModel` |
| `WebBanHang/Controllers/HomeController.cs` | Trang chủ: danh mục + sản phẩm nổi bật + tổng số sản phẩm đang bán |
| `WebBanHang/Controllers/ProductsController.cs` | Danh sách (tìm kiếm + lọc danh mục) và chi tiết sản phẩm kèm tồn kho |

Đăng ký DI trong `Program.cs`:

```csharp
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>(); // chỉ giữ chuỗi kết nối
builder.Services.AddScoped<IProductRepository, SqlProductRepository>();       // mỗi request một instance
```

Các URL đã lấy dữ liệu thật từ SQL Server:

| URL | Nội dung |
| --- | --- |
| `/` | Danh mục + sản phẩm nổi bật (`IsFeatured = 1`) |
| `/Products` | Tìm theo tên/mã (`keyword`) và lọc theo danh mục (`categoryId`) |
| `/Products/Detail/1` | Chi tiết sản phẩm + bảng size/màu + tồn kho thực tế |

Ghi chú kỹ thuật:

- Từ khóa tìm kiếm được **escape** `%`, `_`, `[`, `\` trước khi đưa vào `LIKE ... ESCAPE '\'` → gõ `%` không quét cả bảng, không lỗi.
- Tồn kho tính bằng truy vấn con trên `ProductVariants` (không phụ thuộc view `vw_ProductStock`) nên web vẫn chạy trên database chưa chạy migration.
- Sản phẩm chưa có `ImageUrl` tự dùng ảnh mặc định `/images/no-image.svg`.
- Giá hiển thị theo định dạng Việt Nam (`vi-VN`) — ví dụ `189.000 đ`.
- Thiếu biến môi trường mật khẩu: web vẫn khởi động nhưng ghi **cảnh báo** rõ ràng ra console (không im lặng lỗi 500).

---

## 4. Chuyển từ container tạo tay (`docker run`) sang Docker Compose

Nếu máy bạn **đã có** container `mssql2022` tạo bằng `docker run` trước đó, chỉ cần xóa container (volume `mssql_data` **vẫn được giữ**, dữ liệu không mất) rồi bật lại bằng compose:

```powershell
docker rm -f mssql2022
docker compose up -d
```

`docker-compose.yml` đã khai báo `volumes.mssql_data.name: mssql_data` nên compose dùng lại đúng volume cũ, không tạo volume mới.

---

## 5. Lệnh thường dùng

| Mục đích | Lệnh |
| --- | --- |
| Bật database | `docker compose up -d` |
| Tắt database (giữ dữ liệu) | `docker compose down` |
| Xem trạng thái | `docker compose ps` |
| Xem log SQL Server | `docker compose logs -f sqlserver` |
| Mở sqlcmd tương tác | `docker exec -it mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "<pass>" -C -d FashionShopDb` |
| Kiểm tra compose hợp lệ | `docker compose config` |

---

## 6. Khắc phục sự cố

| Hiện tượng | Nguyên nhân & cách xử lý |
| --- | --- |
| `Chưa cấu hình biến môi trường FASHIONSHOP_SQL_PASSWORD` | Chưa `setx` hoặc VS/terminal đang mở từ trước khi set → tắt hẳn rồi mở lại (áp dụng cho cả Desktop và Web) |
| `A network-related or instance-specific error` | Container chưa chạy/ chưa healthy → `docker compose ps`, `docker compose logs sqlserver` |
| `Login failed for user 'sa'` | `FASHIONSHOP_SQL_PASSWORD` không khớp `MSSQL_SA_PASSWORD` trong `.env` |
| `Invalid object name 'Products'` | Chưa chạy `CSDL_ShopQuanAo.sql` (Bước 3) hoặc volume bị xóa |
| `Conflict. The container name "/mssql2022" is already in use` | Còn container cũ → `docker rm -f mssql2022` rồi `docker compose up -d` |
| Cổng 1433 bị chiếm | Tắt SQL Server service trên Windows (`services.msc` → SQL Server (MSSQLSERVER)) |
| Cảnh báo `volume "mssql_data" already exists but was not created by Docker Compose` | **Vô hại** — compose đang dùng lại volume tạo trước đó để giữ dữ liệu. Có thể bỏ qua. |

> 🔴 **Cảnh báo:** `docker compose down -v` hoặc `docker volume rm mssql_data` sẽ **xóa sạch database**. Muốn reset dữ liệu thì dùng lệnh đó rồi chạy lại Bước 3.

---

## 7. Cấu trúc thư mục

```
Do An C#/
├─ docker-compose.yml        # Hạ tầng: SQL Server 2022
├─ .env / .env.example       # Mật khẩu SA (file .env không commit)
├─ CSDL_ShopQuanAo.sql       # Script tạo DB + dữ liệu mẫu (máy mới)
├─ Scripts/migrations/       # Migration nâng cấp DB đang có (không mất dữ liệu)
│  └─ 01_harden_database.sql # Ràng buộc + index + view tồn kho + seed biến thể
├─ QuanLyShopQuanAo.slnx     # Solution gồm 2 project
├─ QuanLyDesktop/            # WinForms (admin)
│  ├─ Form1.cs               # Đăng nhập
│  ├─ FormMain.cs            # Dashboard + CRUD sản phẩm
│  ├─ FormDanhMuc.cs         # CRUD danh mục
│  ├─ FormBienTheSanPham.cs  # Size / màu / tồn kho
│  ├─ FormDonHang.cs         # Duyệt đơn hàng
│  └─ DatabaseConfig.cs      # Chuỗi kết nối (đọc từ biến môi trường)
└─ WebBanHang/               # ASP.NET Core MVC (khách hàng)
   ├─ Program.cs             # DI: SqlConnectionFactory + SqlProductRepository
   ├─ appsettings.json       # Database:Server/Name/UserId (KHÔNG chứa mật khẩu)
   ├─ Data/                  # Tầng dữ liệu ADO.NET
   │  ├─ SqlConnectionFactory.cs
   │  ├─ IProductRepository.cs
   │  └─ SqlProductRepository.cs
   ├─ Models/                # Product, ProductVariant, Category, ViewModels
   ├─ Controllers/           # HomeController, ProductsController
   └─ Views/                 # Trang chủ, danh sách, chi tiết sản phẩm
```
