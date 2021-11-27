using Dapper;
using FxCurrencyConverter.CurrencyConverter;
using FxCurrencyConverter.Enums;
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

        public IList<CurrencyConversionResponse> GetFxCurrencyConversionAudit(Guid guid)
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

            IList<CurrencyConversionResponse> responseList = new List<CurrencyConversionResponse>();
            while (reader.Read())
            {
                CurrencyConversionResponse response = new CurrencyConversionResponse
                {
                    Id = (Guid)reader["ID"],
                    CcyPair = (string)reader["CcyPair"],
                    Side = (SideEnum)(int)reader["SideId"],
                    OriginalAmount = (decimal)reader["OriginalAmount"],
                    ConversionResults = (ConversionEnum)(int)reader["ConversionResultsId"],

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
