--CREATE DATABASE [TradeRepository]
--GO

USE [TradeRepository]
GO


CREATE TABLE Side
(
ID int NOT NULL,
Description nvarchar(4) NOT NULL,
CONSTRAINT PK_Side PRIMARY KEY (ID)
);


CREATE TABLE ConversionResults
(
ID int NOT NULL,
Description nvarchar(50) NOT NULL,
CONSTRAINT PK_ConversionResults PRIMARY KEY (ID)
);


CREATE TABLE UserFxCurrencyConversionAudit
(
ID bigint NOT NULL IDENTITY(1,1),
RequestID uniqueidentifier NOT NULL,
UserID bigint NOT NULL,
CcyPair nvarchar(7) NOT NULL,
SideId int NOT NULL FOREIGN KEY REFERENCES Side(ID),
OriginalAmount DECIMAL(18, 8) NOT NULL,
ConversionResultsId int NOT NULL FOREIGN KEY REFERENCES ConversionResults(ID),
ConvertedAmountCcy nvarchar(3) NULL,
ConvertedAmount DECIMAL(18, 8) NULL,
PxUsed DECIMAL(18, 8) NULL,
OriginalAmountCcy nvarchar(3) NULL,
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
(1, 'DuplicateRequest'),
(2, 'UserInactive'),
(3, 'ConversionFailedInvalidCcyPair'),
(4, 'ConversionFailedIncorrectMinimumTradingAmount'),
(5, 'ConversionFailedInsufficientBalance'),
(6, 'ConversionFailedInvalidAmount'),
(7, 'ConversionFailedInvalidRequestId'),
(8, 'ConversionFailedInvalidUserId'),
(9, 'MarketClosed'),
(10,'StalePrice'),
(11, 'Pending'),
(12, 'UnknownError')
GO
