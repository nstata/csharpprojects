USE TradeRepository
GO
select * from dbo.UserFxCurrencyConversionAudit order by ID desc

--SELECT COUNT(*) FROM dbo.[UserFxCurrencyConversionAudit] t 
--WHERE t.RequestID = 'B1457E30-3524-440D-B2F4-0D911403B170' 
--AND t.UserID = 14 AND t.ConversionResultsId <> 11
  