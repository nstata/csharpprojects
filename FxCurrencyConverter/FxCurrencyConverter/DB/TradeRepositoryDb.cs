using FxCurrencyConverter.CurrencyConverter;
using Dapper;
using System.Data.SqlClient;

namespace FxCurrencyConverter.DB
{
    public class TradeRepositoryDb : ITradeRepositoryDb
    {
        private readonly string _sqlConnectionStr = @"Server=(LocalDb)\MSSQLLocalDB;Database=TradeRepository;Trusted_Connection=True;";
        private readonly string _sql =
            @"INSERT INTO dbo.[FxCurrencyConversionAudit] 
(ID, CcyPair, SideId, OriginalAmount, ConversionResultsId, ConvertedAmountCcy, ConvertedAmount, PxUsed, OriginalAmountCcy, LastUpdated) 
VALUES 
(@id, @ccyPair, @sideId, @originalAmount, @conversionResultsId, @convertedAmountCcy, @convertedAmount, @pxUsed, @originalAmountCcy, GETUTCDATE())";

        public bool InsertIntoFxCurrencyConversionAudit(CurrencyConversionResponse response)
        {
            bool success = false;

            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_sql, response);
            return success;
        }
    }
}
