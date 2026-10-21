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
docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$p" -C -i /scripts/CSDL_ShopQuanAo.sql
```

Kiểm tra lại:

```powershell
docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$p" -C -Q "SELECT name FROM sys.tables" -d FashionShopDb -W
```

### Bước 4 — Cấu hình mật khẩu cho app Desktop

`QuanLyDesktop/DatabaseConfig.cs` **không hard-code mật khẩu**, mà đọc từ biến môi trường `FASHIONSHOP_SQL_PASSWORD`:

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
```

Tài khoản mẫu (xem trong `CSDL_ShopQuanAo.sql`):

| Tên đăng nhập | Mật khẩu | Quyền |
| --- | --- | --- |
| `admin` | `123456` | Admin — toàn quyền |
| `nhanvien` | `123456` | Staff — không được thêm/sửa/xóa sản phẩm, danh mục |

---

## 3. Chuyển từ container tạo tay (`docker run`) sang Docker Compose

Nếu máy bạn **đã có** container `mssql2022` tạo bằng `docker run` trước đó, chỉ cần xóa container (volume `mssql_data` **vẫn được giữ**, dữ liệu không mất) rồi bật lại bằng compose:

```powershell
docker rm -f mssql2022
docker compose up -d
```

`docker-compose.yml` đã khai báo `volumes.mssql_data.name: mssql_data` nên compose dùng lại đúng volume cũ, không tạo volume mới.

---

## 4. Lệnh thường dùng

| Mục đích | Lệnh |
| --- | --- |
| Bật database | `docker compose up -d` |
| Tắt database (giữ dữ liệu) | `docker compose down` |
| Xem trạng thái | `docker compose ps` |
| Xem log SQL Server | `docker compose logs -f sqlserver` |
| Mở sqlcmd tương tác | `docker exec -it mssql2022 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "<pass>" -C -d FashionShopDb` |
| Kiểm tra compose hợp lệ | `docker compose config` |

---

## 5. Khắc phục sự cố

| Hiện tượng | Nguyên nhân & cách xử lý |
| --- | --- |
| `Chưa cấu hình biến môi trường FASHIONSHOP_SQL_PASSWORD` | Chưa `setx` hoặc VS đang mở từ trước khi set → tắt VS, mở lại |
| `A network-related or instance-specific error` | Container chưa chạy/ chưa healthy → `docker compose ps`, `docker compose logs sqlserver` |
| `Login failed for user 'sa'` | `FASHIONSHOP_SQL_PASSWORD` không khớp `MSSQL_SA_PASSWORD` trong `.env` |
| `Invalid object name 'Products'` | Chưa chạy `CSDL_ShopQuanAo.sql` (Bước 3) hoặc volume bị xóa |
| `Conflict. The container name "/mssql2022" is already in use` | Còn container cũ → `docker rm -f mssql2022` rồi `docker compose up -d` |
| Cổng 1433 bị chiếm | Tắt SQL Server service trên Windows (`services.msc` → SQL Server (MSSQLSERVER)) |
| Cảnh báo `volume "mssql_data" already exists but was not created by Docker Compose` | **Vô hại** — compose đang dùng lại volume tạo trước đó để giữ dữ liệu. Có thể bỏ qua. |

> 🔴 **Cảnh báo:** `docker compose down -v` hoặc `docker volume rm mssql_data` sẽ **xóa sạch database**. Muốn reset dữ liệu thì dùng lệnh đó rồi chạy lại Bước 3.

---

## 6. Cấu trúc thư mục

```
Do An C#/
├─ docker-compose.yml        # Hạ tầng: SQL Server 2022
├─ .env / .env.example       # Mật khẩu SA (file .env không commit)
├─ CSDL_ShopQuanAo.sql       # Script tạo DB + dữ liệu mẫu
├─ QuanLyShopQuanAo.slnx     # Solution gồm 2 project
├─ QuanLyDesktop/            # WinForms (admin)
│  ├─ Form1.cs               # Đăng nhập
│  ├─ FormMain.cs            # Dashboard + CRUD sản phẩm
│  ├─ FormDanhMuc.cs         # CRUD danh mục
│  ├─ FormBienTheSanPham.cs  # Size / màu / tồn kho
│  ├─ FormDonHang.cs         # Duyệt đơn hàng
│  └─ DatabaseConfig.cs      # Chuỗi kết nối (đọc từ biến môi trường)
└─ WebBanHang/               # ASP.NET Core MVC (khách hàng)
```
