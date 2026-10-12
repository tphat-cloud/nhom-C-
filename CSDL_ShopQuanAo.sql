-- ==============================================================
-- ĐỒ ÁN MÔN: NGÔN NGỮ LẬP TRÌNH C#
-- ĐỀ TÀI: QUẢN LÝ VÀ BÁN QUẦN ÁO THỜI TRANG
-- GVGD: TS. ĐỖ NHƯ TÀI
-- ==============================================================

CREATE DATABASE FashionShopDb;
GO

USE FashionShopDb;
GO

-- 1. BẢNG PHÂN QUYỀN (Roles)
CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) NOT NULL,      -- "Admin", "Staff"
    Description NVARCHAR(200) NULL
);
GO

-- 2. BẢNG TÀI KHOẢN QUẢN TRỊ / NHÂN VIÊN (Users)
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(20) NULL,
    RoleId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
GO

-- 3. BẢNG DANH MỤC QUẦN ÁO (Categories)
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,         -- Áo thun, Sơ mi, Quần Jean...
    Description NVARCHAR(255) NULL,
    ImageUrl NVARCHAR(500) NULL,
    IsActive BIT NOT NULL DEFAULT 1
);
GO

-- 4. BẢNG SẢN PHẨM QUẦN ÁO (Products)
CREATE TABLE Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    ProductCode NVARCHAR(50) NOT NULL UNIQUE,  -- AT-001, SM-002...
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18,2) NOT NULL,
    OriginalPrice DECIMAL(18,2) NULL,
    ImageUrl NVARCHAR(500) NULL,
    CategoryId INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    IsFeatured BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
GO

-- 5. BẢNG BIẾN THỂ SIZE & MÀU (ProductVariants)
CREATE TABLE ProductVariants (
    ProductVariantId INT IDENTITY(1,1) PRIMARY KEY,
    ProductId INT NOT NULL,
    Size NVARCHAR(20) NOT NULL,          -- S, M, L, XL, XXL
    Color NVARCHAR(50) NOT NULL,         -- Trắng, Đen, Be, Xanh...
    StockQuantity INT NOT NULL DEFAULT 0,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId) ON DELETE CASCADE
);
GO

-- 6. BẢNG KHÁCH HÀNG (Customers)
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(100) NULL,
    Address NVARCHAR(255) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- 7. BẢNG ĐƠN ĐẶT HÀNG (Orders)
CREATE TABLE Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    OrderCode NVARCHAR(30) NOT NULL UNIQUE,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    CustomerId INT NULL,
    CustomerName NVARCHAR(100) NOT NULL,
    CustomerPhone NVARCHAR(20) NOT NULL,
    ShippingAddress NVARCHAR(255) NOT NULL,
    Note NVARCHAR(500) NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Chờ xử lý', -- "Chờ xử lý", "Đã duyệt", "Đang giao", "Hoàn thành"
    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT N'COD',
    IsPaid BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
GO

-- 8. BẢNG CHI TIẾT ĐƠN HÀNG (OrderDetails)
CREATE TABLE OrderDetails (
    OrderDetailId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductVariantId INT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Size NVARCHAR(20) NULL,
    Color NVARCHAR(50) NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
GO

-- ==============================================================
-- DỮ LIỆU MẪU BAN ĐẦU (SEED DATA)
-- ==============================================================

-- Quyền
INSERT INTO Roles (RoleName, Description) VALUES 
(N'Admin', N'Quản trị viên toàn quyền hệ thống'),
(N'Staff', N'Nhân viên bán hàng và xử lý đơn');

-- Tài khoản (Mật khẩu: 123456)
INSERT INTO Users (Username, PasswordHash, FullName, Email, PhoneNumber, RoleId, IsActive) VALUES 
(N'admin', N'123456', N'Quản Trị Viên', N'admin@shop.vn', N'0901234567', 1, 1),
(N'nhanvien', N'123456', N'Nguyễn Văn Bán Hàng', N'staff@shop.vn', N'0912345678', 2, 1);

-- Danh mục
INSERT INTO Categories (Name, Description, ImageUrl, IsActive) VALUES 
(N'Áo Thun Nam & Nữ', N'Áo thun cotton thoáng mát', N'https://images.unsplash.com/photo-1521572267360-ee0c2909d518?w=500', 1),
(N'Áo Sơ Mi Công Sở', N'Sơ mi chống nhăn thanh lịch', N'https://images.unsplash.com/photo-1596755094514-f87e34085b2c?w=500', 1),
(N'Quần Jeans Cao Cấp', N'Jeans co giãn tốt', N'https://images.unsplash.com/photo-1541099649105-f69ad21f3246?w=500', 1),
(N'Áo Khoác & Hoodie', N'Áo khoác gió ấm áp', N'https://images.unsplash.com/photo-1556905055-8f358a7a47b2?w=500', 1),
(N'Váy & Đầm Nữ', N'Đầm dự tiệc dạo phố phong cách', N'https://images.unsplash.com/photo-1595777457583-95e059d581b8?w=500', 1);

-- Sản phẩm mẫu
INSERT INTO Products (ProductCode, Name, Description, Price, OriginalPrice, CategoryId, IsActive, IsFeatured) VALUES 
(N'AT-001', N'Áo Thun Cotton Oversize Basic', N'Chất liệu 100% cotton 2 chiều định lượng 250gsm.', 189000, 250000, 1, 1, 1),
(N'SM-002', N'Áo Sơ Mi Oxford Slimfit Trắng', N'Chất vải oxford dày dặn, đứng form thanh lịch.', 299000, 380000, 2, 1, 1),
(N'QJ-003', N'Quần Jeans Nam Dáng Suông Vintage', N'Quần bò xanh retro wash nhẹ tôn dáng trẻ trung.', 385000, 490000, 3, 1, 1),
(N'AK-004', N'Áo Khoác Bomber Lót Dù Unisex', N'Chống nước nhẹ, bo chun cổ tay giữ ấm.', 450000, 550000, 4, 1, 1),
(N'VD-005', N'Đầm Hoa Nhí Cổ Vuông Dáng Dài', N'Họa tiết hoa vintage, voan tơ mềm mại.', 345000, 420000, 5, 1, 1);
GO
