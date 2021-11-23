--CREATE DATABASE [TradeRepository]
--GO

USE [TradeRepository]
GO


CREATE TABLE Side
(
ID int NOT NULL,
Description nvarchar(4) NOT NULL
CONSTRAINT PK_Side PRIMARY KEY (ID)
);


CREATE TABLE ConversionResults
(
ID int NOT NULL,
Description nvarchar(50) NOT NULL
CONSTRAINT PK_ConversionResults PRIMARY KEY (ID)
);


CREATE TABLE FxCurrencyConversionAudit
(
ID uniqueidentifier NOT NULL,
CurrencyPair nvarchar(7) NOT NULL,
SideId int NOT NULL FOREIGN KEY REFERENCES Side(ID),
OriginalAmount DECIMAL(11, 4) NOT NULL,
ConversionResultId int NOT NULL FOREIGN KEY REFERENCES ConversionResults(ID),
ConvertedAmountCurrency nvarchar(3) NULL,
ConvertedAmount DECIMAL(11, 4) NULL,
PxUsed DECIMAL(11, 4) NULL,
OriginalAmountCurrency nvarchar(3) NULL,
LastUpdated datetime,
CONSTRAINT PK_FxCurrencyConversionAudit PRIMARY KEY (ID)
);
GO


INSERT INTO Side
(ID, Description)
VALUES 
(0, 'BUY'),
(1, 'SELL');
GO


INSERT INTO ConversionResults
(ID,Description)
VALUES 
(0, 'Successful'),
(1, 'ConversionFailedInvalidCcyPair'),
(2, 'ConversionFailedInvalidAmount'),
(3, 'MarketClosed'),
(4, 'StalePrice');
GO
