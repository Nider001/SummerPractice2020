USE [master]
GO

CREATE DATABASE PracticeDatabase
GO

USE PracticeDatabase
GO

CREATE TABLE [Users] (
	Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_USERS PRIMARY KEY, 
	Login varchar(255) NOT NULL UNIQUE,
	HashPassword binary(4) NOT NULL,
	Name varchar(255) NOT NULL,
	DateOfBirth date NOT NULL,
	Info varchar(2550) NOT NULL,
	IsAdmin BIT NOT NULL,
)
GO
CREATE TABLE [Shops] (
	Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SHOPS PRIMARY KEY, 
	Name varchar(255) NOT NULL,
	TypeId int NOT NULL,
	AddressId int NOT NULL,
)
GO
CREATE TABLE [Addresses] (
	Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_ADDRESSES PRIMARY KEY, 
	City varchar(255) NOT NULL,
	Street varchar(255) NOT NULL,
	Building varchar(10) NOT NULL,
)
CREATE TABLE [Ratings] (
	ShopId int NOT NULL,
	UserId int NOT NULL,
	Rating int NOT NULL
)
CREATE UNIQUE CLUSTERED INDEX AK_RATINGS
   ON Ratings (ShopId, UserId);
GO
CREATE TABLE [ShopTypes] (
	Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_SHOPTYPES PRIMARY KEY, 
	Name varchar(255) NOT NULL,
)
GO

ALTER TABLE [Shops] WITH CHECK ADD CONSTRAINT [Shops_fk0] FOREIGN KEY ([TypeId]) REFERENCES [ShopTypes]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Shops] CHECK CONSTRAINT [Shops_fk0]
GO
ALTER TABLE [Shops] WITH CHECK ADD CONSTRAINT [Shops_fk1] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Shops] CHECK CONSTRAINT [Shops_fk1]
GO
ALTER TABLE [Ratings] WITH CHECK ADD CONSTRAINT [Ratings_fk0] FOREIGN KEY ([ShopId]) REFERENCES [Shops]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Ratings] CHECK CONSTRAINT [Ratings_fk0]
GO
ALTER TABLE [Ratings] WITH CHECK ADD CONSTRAINT [Ratings_fk1] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Ratings] CHECK CONSTRAINT [Ratings_fk1]
GO

SET NOCOUNT ON;  
GO

CREATE PROCEDURE GetShopRatingByName
@ShopName varchar(255),  
@ShopRating float OUTPUT  
AS
	DECLARE @ShopId int
	SELECT @ShopId = Id FROM Shops WHERE Name = @ShopName
    SELECT @ShopRating = CAST(SUM(Rating) AS float)/COUNT(Rating) FROM Ratings WHERE ShopId = @ShopId
RETURN  
GO  

CREATE PROCEDURE GetShopRatingById
@ShopId int,  
@ShopRating float OUTPUT  
AS
    SELECT @ShopRating = CAST(SUM(Rating) AS float)/COUNT(Rating) FROM Ratings WHERE ShopId = @ShopId
RETURN  
GO

CREATE PROCEDURE HandleShopAddress
@City varchar(255),
@Street varchar(255),
@Building varchar(10)
AS
DECLARE @Index AS int
IF EXISTS(SELECT * FROM Addresses WHERE City = @City AND Street = @Street AND Building = @Building)
    BEGIN  
        SELECT @Index = Id FROM Addresses WHERE City = @City AND Street = @Street AND Building = @Building
        RETURN @Index
    END  
ELSE  
    BEGIN  
        INSERT INTO Addresses(City, Street, Building) VALUES (@City, @Street, @Building)
		SELECT @Index = Id FROM Addresses WHERE City = @City AND Street = @Street AND Building = @Building
        RETURN @Index
    END;  
GO

CREATE PROCEDURE HandleShopType
@Name varchar(255)
AS
DECLARE @Index AS int
IF EXISTS(SELECT * FROM ShopTypes WHERE Name = @Name)
    BEGIN  
        SELECT @Index = Id FROM ShopTypes WHERE Name = @Name
        RETURN @Index
    END  
ELSE  
    BEGIN  
        INSERT INTO ShopTypes(Name) VALUES (@Name)
		SELECT @Index = Id FROM ShopTypes WHERE Name = @Name
        RETURN @Index
    END;
GO
