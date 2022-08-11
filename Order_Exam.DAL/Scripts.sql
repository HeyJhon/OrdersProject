CREATE TABLE [dbo].[Order](
	[OrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[DateTransaction] [datetime] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[State] [smallint] NOT NULL,
	[OrderStatusId] [smallint] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
([OrderId] ASC)) 
GO

CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductSku] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[OrderId] [bigint] NOT NULL,
 CONSTRAINT [PK_OrderDetail] PRIMARY KEY CLUSTERED 
([OrderDetailId] ASC)) 
GO

CREATE TABLE [dbo].[OrderStatus](
	[OrderStatusId] [smallint] NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Sort] [smallint] NOT NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
([OrderStatusId] ASC)) 
GO
CREATE TABLE [dbo].[Product](
	[Sku] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL,
	[State] [smallint] NOT NULL,
	[StockLimitAlert] [int] NOT NULL,
	[Image][nvarchar](300)
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
([Sku] ASC)) 
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatus] ([OrderStatusId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductSku])
REFERENCES [dbo].[Product] ([Sku])
GO
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name], [Description], [Sort]) VALUES (1, N'Pending', N'Pending', 1)
GO
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name], [Description], [Sort]) VALUES (2, N'In Proccess', N'In Proccess', 2)
GO
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name], [Description], [Sort]) VALUES (3, N'Completed', N'Completed', 3)
GO
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name], [Description], [Sort]) VALUES (4, N'Delivered', N'Delivered', 4)
GO
INSERT [dbo].[OrderStatus] ([OrderStatusId], [Name], [Description], [Sort]) VALUES (5, N'Canceled', N'Canceled', 5)
GO


CREATE PROCEDURE spListOrderStatus
AS
BEGIN
	SELECT OrderStatusId, Name, Description, Sort FROM OrderStatus ORDER BY Sort
END
GO

CREATE PROCEDURE [dbo].[spInsertOrderStatus]
@OrderStatusId [smallint],
@Name [nvarchar](30),
@Description [nvarchar](100),
@Sort [smallint]
AS
BEGIN
	INSERT INTO [dbo].[OrderStatus](OrderStatusId,Name, Description, Sort)
	VALUES (@OrderStatusId,@Name,@Description,@Sort)

	SELECT @@ROWCOUNT AS Result

END
GO

CREATE PROCEDURE [dbo].[spFindtOrderStatusById]
@OrderStatusId [smallint]
AS
BEGIN
	SELECT OrderStatusId,Name, Description, Sort FROM [dbo].[OrderStatus]
	WHERE OrderStatusId = @OrderStatusId
END
GO

CREATE PROCEDURE [dbo].[spUpdateOrderStatus]
@OrderStatusId [smallint],
@Name [nvarchar](30),
@Description [nvarchar](100),
@Sort [smallint]
AS
BEGIN
	UPDATE [dbo].[OrderStatus] SET
	Name = @Name,
	Description = @Description,
	Sort = @Sort
	WHERE OrderStatusId = @OrderStatusId

	SELECT @@ROWCOUNT AS Result
END
GO

CREATE PROCEDURE [dbo].[spDeleteOrderStatus]
@OrderStatusId [smallint]
AS
BEGIN
	DELETE FROM [dbo].[OrderStatus] WHERE OrderStatusId = @OrderStatusId
	SELECT @@ROWCOUNT AS Result
END
GO

CREATE PROCEDURE [dbo].[spInsertProduct]
	@Sku [nvarchar](50),
	@Name [nvarchar](50),
	@Price [decimal](18,2),
	@Stock [int],
	@State [smallint],
	@StockLimitAlert[int],
	@Image[nvarchar](300)
AS
BEGIN
	IF((SELECT COUNT(*) FROM [dbo].[Product] WHERE Sku = @Sku) > 0)
		BEGIN
			SELECT 0 AS Result
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[Product]
			   ([Sku]
			   ,[Name]
			   ,[Price]
			   ,[Stock]
			   ,[State]
			   ,[StockLimitAlert]
			   ,[Image])
			VALUES (@Sku,@Name,@Price,@Stock,@State,@StockLimitAlert,@Image)
			SELECT @@ROWCOUNT AS Result
		END
END
GO

CREATE PROCEDURE [dbo].[spFindProductBySku]
	@Sku [nvarchar](50)
AS
BEGIN
	SELECT 
		Sku,Name,Price,Stock,State,StockLimitAlert
	FROM 
		[dbo].[Product]
	WHERE Sku = @Sku AND State = 1
END
GO

CREATE PROCEDURE [dbo].[spUpdateProduct]
	@Sku [nvarchar](50),
	@Name [nvarchar](50),
	@Price [decimal](18,2),
	@StockLimitAlert[int],
	@Image[nvarchar](300)
AS
BEGIN
	UPDATE [dbo].[Product] SET
	Name = @Name,
	Price = @Price,
	StockLimitAlert = @StockLimitAlert,
	Image= @Image
	WHERE Sku = @Sku

	 SELECT @@ROWCOUNT AS Result
END
GO


CREATE PROCEDURE [dbo].[spDeleteProduct]
	@Sku [nvarchar](50)
AS
BEGIN
	UPDATE [dbo].[Product] SET
	State = 0
	WHERE Sku = @Sku

	 SELECT @@ROWCOUNT AS Result
END
GO

CREATE PROCEDURE [dbo].[spListProduct]
AS
BEGIN
	SELECT [Sku]
      ,[Name]
      ,[Price]
      ,[Stock]
      ,[State]
      ,[StockLimitAlert]
	  ,[Image]
  FROM [dbo].[Product]
END
GO

CREATE PROCEDURE [dbo].[spInsertOrder]
	@CustomerId [int],
	@UserId [int]
AS
BEGIN
	INSERT INTO [dbo].[Order]
           ([DateTransaction]
           ,[CustomerId]
           ,[UserId]
           ,[Total]
           ,[State]
           ,[OrderStatusId])
     VALUES
           (GETDATE(),@CustomerId,@UserId,0,1,1)

	SELECT @@IDENTITY  AS OrderId
END
GO


CREATE PROCEDURE [dbo].[spInsertOrderDetail]
	@OrderId [bigint],
	@ProductSku [nvarchar](50),
	@Quantity [int]
AS
BEGIN

	IF(((SELECT Stock FROM Product WHERE Sku = @ProductSku) - @Quantity ) < 0 )
		SELECT 0  AS Result
	ELSE
	BEGIN
		UPDATE Product
		SET Stock = Stock - @Quantity
		WHERE Sku = @ProductSku

		INSERT INTO [dbo].[OrderDetail]
           ([ProductSku]
           ,[Quantity]
           ,[Price]
           ,[OrderId])
     VALUES (
			@ProductSku,
			@Quantity,
			(SELECT Price FROM Product WHERE Sku = @ProductSku),
			@OrderId
			)
	SELECT @@ROWCOUNT AS Result
	END
END
GO

CREATE PROCEDURE [dbo].[spSetOrderStatus]
	@StatusStart [smallint],
	@StatusEnd [smallint],
	@OrderId BIGINT 
AS
BEGIN
		UPDATE [Order] SET
		OrderStatusId = @StatusEnd
		WHERE OrderId = @OrderId

		SELECT @@ROWCOUNT AS Result;
END
GO

INSERT INTO [dbo].[Product]([Sku],[Name],[Price],[Stock],[State],[StockLimitAlert],[Image]) VALUES (NEWID(),'Burger',20.00,50,1,5,'Burger')
GO
INSERT INTO [dbo].[Product]([Sku],[Name],[Price],[Stock],[State],[StockLimitAlert],[Image]) VALUES (NEWID(),'Soda',15.00,50,1,5,'Soda')
GO
INSERT INTO [dbo].[Product]([Sku],[Name],[Price],[Stock],[State],[StockLimitAlert],[Image]) VALUES (NEWID(),'Fries',10.00,50,1,5,'Fries')
GO

CREATE PROCEDURE [dbo].[spListOrders]
AS
BEGIN
	SELECT O.OrderId, D.ProductSku, P.Name AS 'ProductName', D.Quantity, D.Price, D.Quantity*D.Price AS 'SubTotal', O.OrderStatusId FROM [Order] AS O
	LEFT JOIN OrderDetail D ON D.OrderId = O.OrderId
	LEFT JOIN Product P ON D.ProductSku = P.Sku
	WHERE O.OrderStatusId < 5

END
GO
