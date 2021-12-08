using Dapper;
using UserFxCurrencyConverter.UserCurrencyConverter;
using UserFxCurrencyConverter.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FxCurrencyConverterIntegrationTests.DB
{
    public class TestTradeRepositoryDb
    {
        private readonly string _sqlConnectionStr = @"Server=(LocalDb)\MSSQLLocalDB;Database=TradeRepository;Trusted_Connection=True;";
        private readonly string _selectSqlQuery = "select * from dbo.UserFxCurrencyConversionAudit t where t.RequestId = @requestId AND t.UserId = @userId ORDER BY t.ID DESC";
        private readonly string _deleteSqlQuery = "delete dbo.UserFxCurrencyConversionAudit";
        private readonly string _insertSql =
            @"INSERT INTO dbo.[UserFxCurrencyConversionAudit] 
(RequestID, UserId, CcyPair, SideId, OriginalAmount, ConversionResultsId, LastUpdated) 
VALUES 
(@requestID, @userId, @ccyPair, @sideId, @originalAmount, @conversionResultsId, GETUTCDATE())";

        public void CleanTable()
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_deleteSqlQuery);
        }

        public IList<UserCurrencyConversionResponse> GetFxCurrencyConversionAudit(Guid RequestId, long UserId)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            using SqlCommand cmd = new SqlCommand(_selectSqlQuery, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@requestId", SqlDbType.UniqueIdentifier).Value = RequestId;
            cmd.Parameters.Add("@userId", SqlDbType.BigInt).Value = UserId;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if(reader == null || !reader.HasRows)
            {
                return null;
            }

            IList<UserCurrencyConversionResponse> responseList = new List<UserCurrencyConversionResponse>();
            while (reader.Read())
            {
                UserCurrencyConversionResponse response = new UserCurrencyConversionResponse
                {
                    RequestId = (Guid)reader["RequestID"],
                    UserId = (long)reader["UserId"],
                    CcyPair = (string)reader["CcyPair"],
                    Side = (UserSideEnum)(int)reader["SideId"],
                    OriginalAmount = (decimal)reader["OriginalAmount"],
                    ConversionResults = (UserConversionEnum)(int)reader["ConversionResultsId"],

                    ConvertedAmount = GetDefaultDecimal(reader["ConvertedAmount"]),
                    PxUsed = GetDefaultDecimal(reader["PxUsed"]),
                    ConvertedAmountCcy = GetDefaultString(reader["ConvertedAmountCcy"]),
                    OriginalAmountCcy = GetDefaultString(reader["OriginalAmountCcy"]),
                };

                responseList.Add(response);
            }

            return responseList;
        }

        public void InsertIntoFxCurrencyConversionAudit(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount, int conversionResultId)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_insertSql, new
            {
                RequestID = requestId,
                UserId = userId,
                CcyPair = ccyPair,
                SideId = isBuy ? (int)UserSideEnum.Buy : (int)UserSideEnum.Sell,
                OriginalAmount = amount,
                ConversionResultsId = conversionResultId
            });
        }


        private decimal? GetDefaultDecimal(object o)
        {
            if (o == DBNull.Value)
            {
                return null;
            }

            return (decimal)o;
        }

        private string GetDefaultString(object o)
        {
            if (o == DBNull.Value)
            {
                return null;
            }

            return (string)o;
        }

    }
}
