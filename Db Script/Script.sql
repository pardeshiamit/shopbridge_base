
Create table CategoryMaster(
CategoryId int Primary Key identity(1,1),
CategoryName nvarchar(50) not null
)
GO

------------------------------------

Create table UnitMaster(
UnitId int Primary Key identity(1,1),
UnitName nvarchar(50) not null
)
GO

------------------------------------

Create table ProductMaster(
ProductId int Primary Key identity(1,1),
CategoryId int not null,
ProductName nvarchar(50) not null,
Description nvarchar(max) NULL,
Price decimal(18, 2) not null,
UnitId int not null,
CONSTRAINT FK_ProductMaster_CategoryMaster_CategoryId FOREIGN KEY (CategoryId)
REFERENCES CategoryMaster(CategoryId),
CONSTRAINT FK_ProductMaster_UnitMaster_UnitId FOREIGN KEY (UnitId)
REFERENCES UnitMaster(UnitId)
)
GO
