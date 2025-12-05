-- Bảng người dùng 
CREATE DATABASE Quanli;
GO
USE Quanli;
GO


CREATE TABLE [Users] (
 UserID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
 Username NVARCHAR(100) NOT NULL UNIQUE, 
 Password NVARCHAR(255) NOT NULL, 
 Role NVARCHAR(50),
 Fullname NVARCHAR(100), 

);
-- Bảng phân loại 
CREATE TABLE [Categories](
   CategoryID INT IDENTITY(1,1) NOT NULL,
   CategoryName NVARCHAR(120) PRIMARY KEY NOT NULL, 
   image_add VARCHAR(MAX),
   CreatedDate DATETIME DEFAULT (getdate()) NULL,

);
-- Bảng sản phẩm 
CREATE TABLE [Products] (
  ProductID INT IDENTITY(1,1) ,
  ProductQR NVARCHAR(50) PRIMARY KEY NOT NULL,
  ProductName NVARCHAR(200) NOT NULL,
  Category NVARCHAR(120), 
  Unit INT,
  Price DECIMAL(18,2) NOT NULL DEFAULT 0.00, 
  SupplierID NVARCHAR(300),
  Status NVARCHAR(50),
  DateInsert DATETIME NULL,
  Image VARCHAR(MAX),
  FOREIGN KEY (Category) REFERENCES Categories(CategoryName),

);
-- Bảng nhà cung cấp 
CREATE TABLE [Suppliers] (
    SupplierID NVARCHAR(300) PRIMARY KEY,
    SupplierName NVARCHAR(200) NOT NULL,
    Phone NVARCHAR(20),
    Email NVARCHAR(200),
    Address NVARCHAR(300),
    ID INT IDENTITY(1,1) NOT NULL,
);

ALTER TABLE Products
ADD FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID);



 CREATE TABLE [Employees] (
    EmployeeID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserID INT,
    Fullname NVARCHAR(100), 
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
 
 );
 CREATE TABLE [Admin] (
    AdminID INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    UserID INT,
    Fullname NVARCHAR(100), 
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
 
 );




-- Bảng khách hàng 
CREATE TABLE [Customers] (
   ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, 
   CustomerID INT NOT NULL,
   ProductQR NVARCHAR(50),
   TotalPrice DECIMAL(18,2) NULL,
   Amount DECIMAL(18,2) NULL,
   Change DECIMAL(18,2) NULL,
   OrderDate DATETIME NULL,
   CustomerName NVARCHAR(200) NOT NULL, 
   Phone NVARCHAR(20),
   Email NVARCHAR(200),
   FOREIGN KEY (ProductQR) REFERENCES Products(ProductQR),
);



-- Bảng chi tiết đơn hàng
CREATE TABLE Orders (
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    ProductQR NVARCHAR(50) NOT NULL,
    ProductName NVARCHAR(255),
    Category NVARCHAR(100),
    CustomerID INT NOT NULL,
    Quantity INT NULL,
    UnitPrice DECIMAL(18,2)  NULL,
    TotalPrice DECIMAL(18,2) NULL,
    OrderDate DATETIME NULL,
    FOREIGN KEY (ProductQR) REFERENCES Products(ProductQR),
    
);

