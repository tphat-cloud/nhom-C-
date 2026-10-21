-- ==============================================================
-- MIGRATION 01: CHUẨN HÓA RÀNG BUỘC + CHỈ MỤC + DỮ LIỆU TỒN KHO
-- Chạy trên database FashionShopDb ĐANG CÓ DỮ LIỆU (không xóa gì).
--
-- Cách chạy (script đã được mount vào container):
--   docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd ^
--     -S localhost -U sa -P "<mat-khau>" -C -i /scripts/migrations/01_harden_database.sql
--
-- Script AN TOÀN để chạy nhiều lần (idempotent):
-- mỗi đối tượng đều được kiểm tra tồn tại trước khi tạo.
-- ==============================================================

USE FashionShopDb;
GO

/* ------------------------------------------------------------
   1. CHECK CONSTRAINTS - chặn dữ liệu vô lý ở tầng database
   (code có kiểm tra, nhưng DB phải là chốt chặn cuối cùng)
   ------------------------------------------------------------ */

-- Giá bán phải > 0, giá gốc không được âm
IF OBJECT_ID(N'CK_Products_Price', N'C') IS NULL
    ALTER TABLE Products ADD CONSTRAINT CK_Products_Price CHECK (Price > 0);
GO
IF OBJECT_ID(N'CK_Products_OriginalPrice', N'C') IS NULL
    ALTER TABLE Products ADD CONSTRAINT CK_Products_OriginalPrice
        CHECK (OriginalPrice IS NULL OR OriginalPrice >= 0);
GO

-- Tồn kho không bao giờ được âm
IF OBJECT_ID(N'CK_ProductVariants_StockQuantity', N'C') IS NULL
    ALTER TABLE ProductVariants ADD CONSTRAINT CK_ProductVariants_StockQuantity
        CHECK (StockQuantity >= 0);
GO

-- Tiền của đơn hàng không âm
IF OBJECT_ID(N'CK_Orders_TotalAmount', N'C') IS NULL
    ALTER TABLE Orders ADD CONSTRAINT CK_Orders_TotalAmount CHECK (TotalAmount >= 0);
GO

-- Trạng thái đơn chỉ được nằm trong luồng nghiệp vụ đã định nghĩa
IF OBJECT_ID(N'CK_Orders_Status', N'C') IS NULL
    ALTER TABLE Orders ADD CONSTRAINT CK_Orders_Status
        CHECK (Status IN (N'Chờ xử lý', N'Đã duyệt', N'Đang giao', N'Hoàn thành', N'Đã hủy'));
GO

-- Phương thức thanh toán chỉ nhận các giá trị hệ thống hỗ trợ
IF OBJECT_ID(N'CK_Orders_PaymentMethod', N'C') IS NULL
    ALTER TABLE Orders ADD CONSTRAINT CK_Orders_PaymentMethod
        CHECK (PaymentMethod IN (N'COD', N'Tiền mặt', N'Chuyển khoản'));
GO

-- Chi tiết đơn: số lượng > 0, đơn giá và thành tiền không âm
IF OBJECT_ID(N'CK_OrderDetails_Quantity', N'C') IS NULL
    ALTER TABLE OrderDetails ADD CONSTRAINT CK_OrderDetails_Quantity CHECK (Quantity > 0);
GO
IF OBJECT_ID(N'CK_OrderDetails_UnitPrice', N'C') IS NULL
    ALTER TABLE OrderDetails ADD CONSTRAINT CK_OrderDetails_UnitPrice CHECK (UnitPrice >= 0);
GO
IF OBJECT_ID(N'CK_OrderDetails_TotalPrice', N'C') IS NULL
    ALTER TABLE OrderDetails ADD CONSTRAINT CK_OrderDetails_TotalPrice CHECK (TotalPrice >= 0);
GO

/* ------------------------------------------------------------
   2. UNIQUE: mỗi sản phẩm chỉ có 1 biến thể cho mỗi cặp Size + Màu
   (code đã kiểm tra trùng, nhưng 2 máy cùng thêm sẽ lọt nếu thiếu ràng buộc này)
   ------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'UQ_ProductVariants_Product_Size_Color'
                 AND object_id = OBJECT_ID(N'ProductVariants'))
    CREATE UNIQUE INDEX UQ_ProductVariants_Product_Size_Color
        ON ProductVariants (ProductId, Size, Color);
GO

/* ------------------------------------------------------------
   3. INDEX phục vụ tra cứu / lọc / join
   ------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_ProductVariants_ProductId'
                 AND object_id = OBJECT_ID(N'ProductVariants'))
    CREATE INDEX IX_ProductVariants_ProductId ON ProductVariants (ProductId);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_Products_CategoryId'
                 AND object_id = OBJECT_ID(N'Products'))
    CREATE INDEX IX_Products_CategoryId ON Products (CategoryId) INCLUDE (IsActive);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_Orders_Status_OrderDate'
                 AND object_id = OBJECT_ID(N'Orders'))
    CREATE INDEX IX_Orders_Status_OrderDate ON Orders (Status, OrderDate DESC);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_Orders_CustomerPhone'
                 AND object_id = OBJECT_ID(N'Orders'))
    CREATE INDEX IX_Orders_CustomerPhone ON Orders (CustomerPhone);
GO

IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_OrderDetails_OrderId'
                 AND object_id = OBJECT_ID(N'OrderDetails'))
    CREATE INDEX IX_OrderDetails_OrderId ON OrderDetails (OrderId) INCLUDE (ProductId, Quantity);
GO

/* ------------------------------------------------------------
   4. VIEW tổng hợp tồn kho theo sản phẩm
   Dùng cho: lưới sản phẩm ở Desktop, kiểm tra "còn hàng" ở Web
   ------------------------------------------------------------ */
CREATE OR ALTER VIEW vw_ProductStock
AS
SELECT
    p.ProductId,
    p.ProductCode,
    p.Name,
    p.Price,
    p.CategoryId,
    p.IsActive,
    ISNULL(SUM(v.StockQuantity), 0) AS TotalStock,
    COUNT(v.ProductVariantId)        AS VariantCount
FROM Products p
LEFT JOIN ProductVariants v ON v.ProductId = p.ProductId
GROUP BY p.ProductId, p.ProductCode, p.Name, p.Price, p.CategoryId, p.IsActive;
GO

/* ------------------------------------------------------------
   5. DỮ LIỆU TỒN KHO MẪU (30 biến thể cho 5 sản phẩm seed)
   Chỉ nạp khi bảng ProductVariants còn trống để không ghi đè dữ liệu thật.
   ------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM ProductVariants)
BEGIN
    INSERT INTO ProductVariants (ProductId, Size, Color, StockQuantity)
    SELECT p.ProductId, v.Size, v.Color, v.StockQuantity
    FROM (VALUES
        -- Áo thun oversize: 4 size x 2 màu
        (N'AT-001', N'S',  N'Trắng', 25), (N'AT-001', N'M',  N'Trắng', 30),
        (N'AT-001', N'L',  N'Trắng', 20), (N'AT-001', N'XL', N'Trắng', 10),
        (N'AT-001', N'S',  N'Đen',   15), (N'AT-001', N'M',  N'Đen',   22),
        (N'AT-001', N'L',  N'Đen',   18), (N'AT-001', N'XL', N'Đen',    8),
        -- Sơ mi oxford: 3 size x 2 màu
        (N'SM-002', N'S',  N'Trắng', 12), (N'SM-002', N'M',  N'Trắng', 18),
        (N'SM-002', N'L',  N'Trắng', 15), (N'SM-002', N'S',  N'Xanh',    8),
        (N'SM-002', N'M',  N'Xanh',  10), (N'SM-002', N'L',  N'Xanh',    6),
        -- Quần jeans: 4 size x 1 màu
        (N'QJ-003', N'29', N'Xanh đậm', 10), (N'QJ-003', N'30', N'Xanh đậm', 14),
        (N'QJ-003', N'31', N'Xanh đậm', 12), (N'QJ-003', N'32', N'Xanh đậm',  6),
        -- Áo khoác bomber: 3 size x 2 màu
        (N'AK-004', N'M',  N'Đen',  9), (N'AK-004', N'L', N'Đen', 11),
        (N'AK-004', N'XL', N'Đen',  5), (N'AK-004', N'M', N'Be',   7),
        (N'AK-004', N'L',  N'Be',   8), (N'AK-004', N'XL', N'Be',  4),
        -- Đầm hoa nhí: 3 size x 2 màu
        (N'VD-005', N'S',  N'Hồng', 6), (N'VD-005', N'M', N'Hồng', 9),
        (N'VD-005', N'L',  N'Hồng', 5), (N'VD-005', N'S', N'Đen',   4),
        (N'VD-005', N'M',  N'Đen',  7), (N'VD-005', N'L', N'Đen',   3)
    ) AS v (ProductCode, Size, Color, StockQuantity)
    JOIN Products p ON p.ProductCode = v.ProductCode;

    PRINT N'Đã nạp dữ liệu tồn kho mẫu.';
END
ELSE
    PRINT N'Bảng ProductVariants đã có dữ liệu - bỏ qua bước nạp tồn kho mẫu.';
GO

PRINT N'=== MIGRATION 01 HOAN TAT ===';
GO
