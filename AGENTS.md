# AGENTS.md — Hướng dẫn cho AI agent làm việc trong repo này

> File này để agent (ở IDE của mỗi thành viên) tự đọc trước khi code.
> Nhiệm vụ & phân công: xem `docs/project-management/TASK_ASSIGNMENT.md`.
> Mẫu prompt giao việc: xem `docs/project-management/AGENT_PROMPTS.md`.

## 1. Tổng quan hệ thống
Đồ án C# "Fashion Shop" — **2 ứng dụng dùng chung 1 cơ sở dữ liệu**:

| Thành phần | Công nghệ | Vai trò |
|---|---|---|
| `QuanLyDesktop/` | WinForms (.NET 8, ADO.NET) | Admin/Staff: đăng nhập, sản phẩm, danh mục, biến thể size/màu, phiếu nhập kho, duyệt đơn, dashboard |
| `WebBanHang/` | ASP.NET Core MVC (.NET 8) | Khách: xem sản phẩm, giỏ hàng, đặt hàng, tra cứu đơn |
| SQL Server 2022 | Docker (`docker compose`) | CSDL `FashionShopDb` dùng chung |

- **Tầng dữ liệu là ADO.NET thuần** (`Microsoft.Data.SqlClient`) — **KHÔNG dùng Entity Framework**.
- Solution: `QuanLyShopQuanAo.slnx`.

## 2. Build & chạy
```powershell
dotnet build QuanLyShopQuanAo.slnx              # phải ra 0 warning / 0 error
dotnet run --project QuanLyDesktop              # app quản lý (WinForms)
.\run-web.cmd                                   # web: tự nạp pass từ .env + bật container + mở http://localhost:5193
```

## 3. Môi trường & CSDL
- Bật DB: `docker compose up -d` → chờ `docker compose ps` = healthy.
- Tạo DB lần đầu: chạy `CSDL_ShopQuanAo.sql` qua `sqlcmd` **kèm `-f 65001`** (tiếng Việt).
- Nâng cấp DB cũ (idempotent): `Scripts/migrations/01_harden_database.sql`,
  `02_fix_order_revenue.sql`, `03_stock_receipts.sql`.
- **Không hard-code mật khẩu/chuỗi kết nối.** App đọc biến môi trường
  `FASHIONSHOP_SQL_PASSWORD` (xem `QuanLyDesktop/DatabaseConfig.cs`,
  `WebBanHang/Data/SqlConnectionFactory.cs`). Giá trị phải khớp
  `MSSQL_SA_PASSWORD` trong `.env` (file `.env` đã gitignore — không bao giờ commit).
- Tài khoản mẫu: `admin/123456` (Admin), `nhanvien/123456` (Staff).

## 4. Quy ước code PHẢI tuân thủ
- **SQL tham số hóa 100%** (`@param`), không nối chuỗi từ đầu vào người dùng.
- Đọc cột an toàn khi `NULL` (pattern `ReadInt/ReadText/ReadMoney/...` trong
  `SqlProductRepository.cs`). Truy vấn web dùng `async` + `CancellationToken`.
- **Soft delete**: sản phẩm/danh mục xóa bằng `IsActive = 0`, không `DELETE`.
- **Tồn kho chỉ đổi trong transaction, có điều kiện**:
  - Trừ khi đặt hàng: `UPDATE ... SET Stock = Stock - @q WHERE Stock >= @q`,
    `@@ROWCOUNT = 0` ⇒ **rollback toàn bộ** (khách → đơn → chi tiết → kho 1 transaction).
  - Cộng khi nhập kho; hoàn tồn khi hủy đơn — cùng transaction.
  - Không sửa tồn kho trực tiếp ngoài các luồng trên.
- Tìm kiếm tiếng Việt: dùng `COLLATE Vietnamese_CI_AI` và **escape `% _ [ \`**
  trước khi đưa vào `LIKE ... ESCAPE '\'`.
- Giá hiển thị theo `vi-VN` (vd `189.000 đ`).
- Web: mọi POST có `[ValidateAntiForgeryToken]`; `returnUrl` chỉ nhận
  `Url.IsLocalUrl`; tiền/giỏ đọc lại **từ CSDL**, không tin giá client gửi lên.
- **Mật khẩu phải băm** (PBKDF2/SHA-256+salt). *Hiện tại code còn so sánh
  chuỗi thuần — xem task SEC-01; khi sửa xong thì mọi nơi tạo/đổi mật khẩu
  phải dùng chung util băm, không nơi nào lưu thuần.*
- Không commit artifact sinh tự động (bin/obj, `__pycache__`, ảnh preview…),
  và không commit tài liệu/diagram ra nhánh code trừ khi được yêu cầu.

## 5. Kiểm chứng (không được bỏ qua)
- Trước khi báo "xong": `dotnet build` sạch **và** chạy thử chức năng trên
  DB thật (web qua `run-web.cmd`; desktop qua `dotnet run --project QuanLyDesktop`).
- **Không đánh dấu hoàn thành chỉ vì đã viết code / thấy file.**
- Ghi lại kịch bản đã test + kết quả. Nếu thiếu dữ liệu/môi trường để kết luận,
  nói rõ thay vì suy đoán.

## 6. Quy tắc Git (bắt buộc)
- **KHÔNG push thẳng `main`, KHÔNG force-push, KHÔNG tự merge.**
- Mỗi task = 1 branch: `git checkout main; git pull; git checkout -b <ten-nhiem-vu>`
  (vd `web-01-phan-trang`, `des-01-isfeatured`, `sec-01-bam-mat-khau`).
- Commit message rõ theo kiểu Conventional Commits:
  `feat(web): ...`, `fix(desktop): ...`, `docs: ...`, `chore: ...`.
- Kết thúc: **mở Pull Request vào `main`** để trưởng nhóm (Phát) review & merge.
  PR cross-module hoặc liên quan bảo mật ⇒ bắt buộc Phát merge.
- Trước khi sửa, `git status`/`git pull` để chắc chắn không giẫm thay đổi người khác.

## 7. Cấu trúc nhanh
```
QuanLyDesktop/  Form1 (đăng nhập), FormMain (dashboard + CRUD SP),
                FormDanhMuc, FormBienTheSanPham, FormDonHang, FormNhapKho,
                DatabaseConfig.cs
WebBanHang/     Controllers/ (Home, Products, Cart, Checkout, OrderLookup)
                Data/ (SqlConnectionFactory, *Repository), Services/ (Cart),
                Models/, Views/
Scripts/migrations/  01_harden_database.sql, 02_fix_order_revenue.sql, 03_stock_receipts.sql
docs/project-management/  TASK_ASSIGNMENT.md, AGENT_PROMPTS.md
```
