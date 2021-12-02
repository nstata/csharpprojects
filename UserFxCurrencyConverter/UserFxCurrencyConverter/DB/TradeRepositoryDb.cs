using Dapper;
using System;
using System.Data.SqlClient;
using UserFxCurrencyConverter.Enums;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.DB
{
    public class TradeRepositoryDb : ITradeRepositoryDb
    {
        private readonly string _sqlConnectionStr = @"Server=(LocalDb)\MSSQLLocalDB;Database=TradeRepository;Trusted_Connection=True;";

        private readonly string _insertSql =
            @"INSERT INTO dbo.[UserFxCurrencyConversionAudit] 
(RequestID, UserId, CcyPair, SideId, OriginalAmount, ConversionResultsId, LastUpdated) 
VALUES 
(@requestID, @userId, @ccyPair, @sideId, @originalAmount, @conversionResultsId, GETUTCDATE())";

        private readonly string _updateSql =
             @"UPDATE dbo.[UserFxCurrencyConversionAudit] 
SET ConversionResultsId =  @conversionResultsId, ConvertedAmountCcy =  @convertedAmountCcy, ConvertedAmount = @convertedAmount,
PxUsed =  @pxUsed, OriginalAmountCcy = @originalAmountCcy, LastUpdated =  GETUTCDATE()
WHERE RequestID = @requestID AND UserID = @userID";

        private readonly string _selectSql =
             @"SELECT COUNT(*) FROM dbo.[UserFxCurrencyConversionAudit] t WHERE t.RequestID = @requestID AND t.UserID = @userID AND t.ConversionResultsId <> 11";


        public void UpdateFxCurrencyConversionAudit(UserCurrencyConversionResponse response)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_updateSql, response);
        }

        public void InsertIntoFxCurrencyConversionAudit(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_insertSql, new 
            { 
                RequestID = requestId,
                UserId = userId,
                CcyPair = ccyPair,
                SideId = isBuy ? (int) UserSideEnum.Buy : (int) UserSideEnum.Sell,
                OriginalAmount = amount,
                ConversionResultsId = (int) UserConversionEnum.Pending
            });
        }

        public bool IsDuplicateRequest(Guid requestId, long userId)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            int rowsAffected = conn.Execute(_selectSql, new
            {
                RequestID = requestId,
                UserId = userId,
            });

            return rowsAffected > 0;
        }
    }
}
