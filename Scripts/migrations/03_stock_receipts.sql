-- ==============================================================
-- MIGRATION 03: PHIẾU NHẬP KHO (StockReceipts + StockReceiptDetails)
-- Bổ sung chứng từ nhập kho để tồn trong ProductVariants không còn
-- là con số không rõ nguồn gốc, đồng thời lưu được giá nhập (giá vốn).
--
-- Chạy trên database FashionShopDb ĐANG CÓ DỮ LIỆU (không xóa gì).
--
-- Cách chạy (thư mục Scripts/migrations đã được mount vào container):
--   docker exec -i mssql2022 /opt/mssql-tools18/bin/sqlcmd ^
--     -S localhost -U sa -P "<mat-khau>" -C -f 65001 -i /migrations/03_stock_receipts.sql
--
-- Script AN TOÀN để chạy nhiều lần (idempotent).
-- ==============================================================

USE FashionShopDb;
GO

/* ------------------------------------------------------------
   1. BẢNG ĐẦU PHIẾU NHẬP KHO
   ------------------------------------------------------------ */
IF OBJECT_ID(N'StockReceipts', N'U') IS NULL
BEGIN
    CREATE TABLE StockReceipts (
        StockReceiptId INT IDENTITY(1,1) PRIMARY KEY,
        ReceiptCode NVARCHAR(30) NOT NULL UNIQUE,
        ReceiptDate DATETIME NOT NULL DEFAULT GETDATE(),
        SupplierName NVARCHAR(150) NULL,
        SupplierPhone NVARCHAR(20) NULL,
        Note NVARCHAR(500) NULL,
        TotalQuantity INT NOT NULL DEFAULT 0,
        TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
        Status NVARCHAR(30) NOT NULL DEFAULT N'Đã nhập',
        CreatedByUserId INT NULL,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (CreatedByUserId) REFERENCES Users(UserId),
        CONSTRAINT CK_StockReceipts_TotalQuantity CHECK (TotalQuantity >= 0),
        CONSTRAINT CK_StockReceipts_TotalAmount CHECK (TotalAmount >= 0),
        CONSTRAINT CK_StockReceipts_Status CHECK (Status IN (N'Đã nhập', N'Đã hủy'))
    );
    PRINT N'Đã tạo bảng StockReceipts.';
END
GO

/* ------------------------------------------------------------
   2. BẢNG CHI TIẾT PHIẾU NHẬP KHO
   ------------------------------------------------------------ */
IF OBJECT_ID(N'StockReceiptDetails', N'U') IS NULL
BEGIN
    CREATE TABLE StockReceiptDetails (
        StockReceiptDetailId INT IDENTITY(1,1) PRIMARY KEY,
        StockReceiptId INT NOT NULL,
        ProductVariantId INT NOT NULL,
        ProductName NVARCHAR(200) NOT NULL,
        Size NVARCHAR(20) NULL,
        Color NVARCHAR(50) NULL,
        Quantity INT NOT NULL,
        ImportPrice DECIMAL(18,2) NOT NULL,
        TotalPrice DECIMAL(18,2) NOT NULL,
        FOREIGN KEY (StockReceiptId) REFERENCES StockReceipts(StockReceiptId) ON DELETE CASCADE,
        FOREIGN KEY (ProductVariantId) REFERENCES ProductVariants(ProductVariantId),
        CONSTRAINT CK_StockReceiptDetails_Quantity CHECK (Quantity > 0),
        CONSTRAINT CK_StockReceiptDetails_ImportPrice CHECK (ImportPrice > 0),
        CONSTRAINT CK_StockReceiptDetails_TotalPrice CHECK (TotalPrice >= 0)
    );
    PRINT N'Đã tạo bảng StockReceiptDetails.';
END
GO

/* ------------------------------------------------------------
   3. CHỈ MỤC
   ------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'UQ_StockReceiptDetails_Receipt_Variant'
                 AND object_id = OBJECT_ID(N'StockReceiptDetails'))
    CREATE UNIQUE INDEX UQ_StockReceiptDetails_Receipt_Variant
        ON StockReceiptDetails (StockReceiptId, ProductVariantId);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_StockReceipts_ReceiptDate'
                 AND object_id = OBJECT_ID(N'StockReceipts'))
    CREATE INDEX IX_StockReceipts_ReceiptDate ON StockReceipts (ReceiptDate DESC);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_StockReceiptDetails_StockReceiptId'
                 AND object_id = OBJECT_ID(N'StockReceiptDetails'))
    CREATE INDEX IX_StockReceiptDetails_StockReceiptId
        ON StockReceiptDetails (StockReceiptId) INCLUDE (ProductVariantId, Quantity);
GO
IF NOT EXISTS (SELECT 1 FROM sys.indexes
               WHERE name = N'IX_StockReceiptDetails_ProductVariantId'
                 AND object_id = OBJECT_ID(N'StockReceiptDetails'))
    CREATE INDEX IX_StockReceiptDetails_ProductVariantId ON StockReceiptDetails (ProductVariantId);
GO

/* ------------------------------------------------------------
   4. DỮ LIỆU MẪU (chỉ nạp khi chưa có phiếu nào)
   Tồn kho đang có được ghi nhận là kết quả của 2 phiếu nhập,
   nên Quantity của mỗi dòng đúng bằng StockQuantity hiện tại.
   ------------------------------------------------------------ */
IF NOT EXISTS (SELECT 1 FROM StockReceipts)
BEGIN
    INSERT INTO StockReceipts (ReceiptCode, ReceiptDate, SupplierName, SupplierPhone, Note, CreatedByUserId)
    VALUES
    (N'NK261020090000001', DATEADD(DAY, -10, GETDATE()), N'Công ty May mặc Sài Gòn', N'02838123456', N'Nhập đợt 1: áo thun và sơ mi', 1),
    (N'NK261027140000002', DATEADD(DAY, -3,  GETDATE()), N'Xưởng may Bình Dương',    N'02743876543', N'Nhập đợt 2: jeans, áo khoác, đầm', 1);

    INSERT INTO StockReceiptDetails (StockReceiptId, ProductVariantId, ProductName, Size, Color, Quantity, ImportPrice, TotalPrice)
    SELECT r.StockReceiptId, v.ProductVariantId, p.Name, v.Size, v.Color, v.StockQuantity,
           CAST(ROUND(p.Price * 0.6, -3) AS DECIMAL(18,2)),
           v.StockQuantity * CAST(ROUND(p.Price * 0.6, -3) AS DECIMAL(18,2))
    FROM ProductVariants v
    JOIN Products p ON p.ProductId = v.ProductId
    JOIN StockReceipts r ON r.ReceiptCode = N'NK261020090000001'
    WHERE p.ProductCode IN (N'AT-001', N'SM-002')
      AND v.StockQuantity > 0;

    INSERT INTO StockReceiptDetails (StockReceiptId, ProductVariantId, ProductName, Size, Color, Quantity, ImportPrice, TotalPrice)
    SELECT r.StockReceiptId, v.ProductVariantId, p.Name, v.Size, v.Color, v.StockQuantity,
           CAST(ROUND(p.Price * 0.6, -3) AS DECIMAL(18,2)),
           v.StockQuantity * CAST(ROUND(p.Price * 0.6, -3) AS DECIMAL(18,2))
    FROM ProductVariants v
    JOIN Products p ON p.ProductId = v.ProductId
    JOIN StockReceipts r ON r.ReceiptCode = N'NK261027140000002'
    WHERE p.ProductCode IN (N'QJ-003', N'AK-004', N'VD-005')
      AND v.StockQuantity > 0;

    PRINT N'Đã nạp 2 phiếu nhập kho mẫu.';
END
GO

/* ------------------------------------------------------------
   5. ĐỒNG BỘ TỔNG LƯỢNG / TỔNG TIỀN CỦA ĐẦU PHIẾU
   Chạy mỗi lần migration để sửa luôn các phiếu lệch số (nếu có).
   ------------------------------------------------------------ */
UPDATE r
SET TotalQuantity = t.TotalQty,
    TotalAmount   = t.TotalAmt
FROM StockReceipts r
JOIN (
    SELECT StockReceiptId,
           SUM(Quantity)   AS TotalQty,
           SUM(TotalPrice) AS TotalAmt
    FROM StockReceiptDetails
    GROUP BY StockReceiptId
) t ON t.StockReceiptId = r.StockReceiptId;
GO

/* ------------------------------------------------------------
   6. TỰ KIỂM TRA SAU KHI CHẠY
   SoTotalPhaiBang: mỗi phiếu phải có TotalQuantity = tổng Quantity của nó.
   ------------------------------------------------------------ */
SELECT
    r.ReceiptCode,
    r.Status,
    r.TotalQuantity,
    ISNULL(d.TotalQty, 0)  AS DetailQty,
    r.TotalAmount,
    ISNULL(d.TotalAmt, 0)  AS DetailAmt,
    CASE WHEN r.TotalQuantity = ISNULL(d.TotalQty, 0)
          AND r.TotalAmount   = ISNULL(d.TotalAmt, 0)
         THEN N'OK' ELSE N'LECH' END AS KetQua
FROM StockReceipts r
LEFT JOIN (
    SELECT StockReceiptId,
           SUM(Quantity)   AS TotalQty,
           SUM(TotalPrice) AS TotalAmt
    FROM StockReceiptDetails
    GROUP BY StockReceiptId
) d ON d.StockReceiptId = r.StockReceiptId
ORDER BY r.StockReceiptId;
GO

PRINT N'=== MIGRATION 03 HOAN TAT ===';
GO
