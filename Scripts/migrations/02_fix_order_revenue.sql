-- ==============================================================
-- MIGRATION 02: ĐỒNG BỘ CỜ THANH TOÁN VỚI TRẠNG THÁI ĐƠN HÀNG
-- Sửa lỗi doanh thu: đơn đã hủy nhưng vẫn còn IsPaid = 1 thì
-- Dashboard vẫn cộng tiền vào "Đã thu hôm nay".
--
-- Cách chạy (thư mục Scripts/migrations đã được mount vào container):
--   docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd ^
--     -S localhost -U sa -P "<mat-khau>" -C -i /migrations/02_fix_order_revenue.sql
--
-- Script AN TOÀN để chạy nhiều lần (idempotent).
-- ==============================================================

USE FashionShopDb;
GO

/* ------------------------------------------------------------
   1. DỌN DỮ LIỆU CŨ
   Các đơn đã chuyển sang "Đã hủy" trước bản sửa này vẫn giữ
   IsPaid = 1; chuẩn hóa lại để doanh thu không bị đội lên.
   ------------------------------------------------------------ */
DECLARE @cleaned INT;

UPDATE Orders
SET IsPaid = 0
WHERE Status = N'Đã hủy'
  AND IsPaid = 1;

SET @cleaned = @@ROWCOUNT;

PRINT N'Đã xóa cờ thanh toán của ' + CAST(@cleaned AS NVARCHAR(10)) + N' đơn đã hủy.';
GO

/* ------------------------------------------------------------
   2. CHỐT CHẶN Ở TẦNG DATABASE
   Đơn đã hủy thì không thể mang cờ đã thanh toán, kể cả khi
   có công cụ khác ghi thẳng vào bảng Orders.
   ------------------------------------------------------------ */
IF OBJECT_ID(N'CK_Orders_CancelledNotPaid', N'C') IS NULL
    ALTER TABLE Orders WITH CHECK ADD CONSTRAINT CK_Orders_CancelledNotPaid
        CHECK (Status <> N'Đã hủy' OR IsPaid = 0);
GO

/* ------------------------------------------------------------
   3. TỰ KIỂM TRA SAU KHI CHẠY
   CountCancelledPaid phải bằng 0.
   RevenueAllTime phải bằng tổng tiền của các đơn Hoàn thành
   hoặc Đã thanh toán và chưa bị hủy.
   ------------------------------------------------------------ */
SELECT
    SUM(CASE WHEN Status = N'Hoàn thành' OR (IsPaid = 1 AND Status <> N'Đã hủy')
             THEN TotalAmount ELSE 0 END) AS RevenueAllTime,
    SUM(CASE WHEN Status = N'Đã hủy' AND IsPaid = 1 THEN 1 ELSE 0 END) AS CountCancelledPaid
FROM Orders;
GO

PRINT N'=== MIGRATION 02 HOAN TAT ===';
GO
