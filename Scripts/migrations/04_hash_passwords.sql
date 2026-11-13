-- ==============================================================
-- MIGRATION 04: BĂM MẬT KHẨU (PBKDF2-HMAC-SHA256)
-- Thay mật khẩu đang lưu dạng chuỗi thuần của tài khoản mẫu bằng
-- hash PBKDF2 tương ứng. Mật khẩu demo vẫn là 123456.
--
-- Chạy trên database FashionShopDb ĐANG CÓ DỮ LIỆU (không xóa gì).
--
-- Cách chạy (thư mục Scripts/migrations đã được mount vào container):
--   docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd ^
--     -S localhost -U sa -P "<mat-khau>" -C -f 65001 -i /migrations/04_hash_passwords.sql
--
-- Script AN TOÀN để chạy nhiều lần: chỉ cập nhật dòng còn lưu chuỗi thuần.
-- Sau khi chạy, đăng nhập desktop bằng admin/123456 hoặc nhanvien/123456.
-- ==============================================================

USE FashionShopDb;
GO

/* ------------------------------------------------------------
   1. TÀI KHOẢN MẪU
   Mỗi tài khoản dùng salt riêng dù cùng mật khẩu 123456.
   ------------------------------------------------------------ */
UPDATE Users
SET PasswordHash = N'PBKDF2-SHA256$100000$CNieVO3w+2kisep0K/cqQg==$UBqTPbq/t/7Bi8e8oQGyC8I/4KwMk5Ps9Vst4v0e3HA='
WHERE Username = N'admin'
  AND PasswordHash = N'123456';

IF @@ROWCOUNT > 0
    PRINT N'Đã băm mật khẩu tài khoản admin.';
GO

UPDATE Users
SET PasswordHash = N'PBKDF2-SHA256$100000$YJGwr8Cu82zdu+NKzOBuDg==$sYuW50fZtvsTLp/9BxG9RNrmUOh3msZhsYRd6fYlwcI='
WHERE Username = N'nhanvien'
  AND PasswordHash = N'123456';

IF @@ROWCOUNT > 0
    PRINT N'Đã băm mật khẩu tài khoản nhanvien.';
GO

/* ------------------------------------------------------------
   2. KIỂM TRA CÒN TÀI KHOẢN CHƯA BĂM
   Nếu có tài khoản tự tạo trước bản vá này, mật khẩu của nó
   không thể băm tự động trong T-SQL (PBKDF2 cần tính trong C#).
   Hãy đặt lại mật khẩu cho các tài khoản đó bằng công cụ quản trị.
   ------------------------------------------------------------ */
IF EXISTS (SELECT 1 FROM Users WHERE PasswordHash NOT LIKE N'PBKDF2-SHA256$%')
BEGIN
    PRINT N'CẢNH BÁO: vẫn còn tài khoản lưu mật khẩu chưa băm.';
    PRINT N'         Cần đặt lại mật khẩu cho các tài khoản này trước khi đăng nhập.';
END
ELSE
    PRINT N'Tất cả tài khoản đã dùng mật khẩu băm PBKDF2.';
GO
