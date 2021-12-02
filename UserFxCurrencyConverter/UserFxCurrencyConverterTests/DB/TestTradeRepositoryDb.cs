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
        private readonly string _selectSqlQuery = "select * from dbo.FxCurrencyConversionAudit t where t.ID = @id";
        private readonly string _deleteSqlQuery = "delete dbo.FxCurrencyConversionAudit";

        public void CleanTable()
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);
            conn.Execute(_deleteSqlQuery);
        }

        public IList<UserCurrencyConversionResponse> GetFxCurrencyConversionAudit(Guid guid)
        {
            using SqlConnection conn = new SqlConnection(_sqlConnectionStr);

            using SqlCommand cmd = new SqlCommand(_selectSqlQuery, conn);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = guid;

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
