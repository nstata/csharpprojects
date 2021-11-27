using FxCurrencyConverter.CurrencyConverter;
using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.DB;
using FxCurrencyConverter.Enums;
using FxCurrencyConverterIntegrationTests.DB;
using FxCurrencyConverterIntegrationTests.State;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace FxCurrencyConverterIntegrationTests.Steps
{
    [Binding]
    public class FxCurrencyConversionSteps

    {
        private readonly IDataProvider _dataProvider;
        private readonly CurrencyConverterManager _currencyConverterManager;
        private readonly List<TestState> _testStateList = new List<TestState>();
        private readonly TestTradeRepositoryDb _testTradeRepositoryDb = new TestTradeRepositoryDb();

        public FxCurrencyConversionSteps()
        {
            _dataProvider = new TestMarketDataProvider();
            ITradeRepositoryDb tradeRepositoryDb = new TradeRepositoryDb();
            _currencyConverterManager = new CurrencyConverterManager(_dataProvider, tradeRepositoryDb);
        }


        [Given(@"the database is clean")]
        public void GivenTheDatabaseIsClean()
        {
            _testTradeRepositoryDb.CleanTable();
        }



        [Given(@"our input is:")]
        public void GivenOurInputIs(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid guid = Guid.Parse(row["Guid"]);
                string ccyPair = row["CurrencyPair"];
                SideEnum side = row["Side"] == "Buy" ? SideEnum.Buy : SideEnum.Sell;
                decimal amount = decimal.Parse(row["Amount"]);

                TestState testState = new TestState
                {
                    Id = id,
                    Guid = guid,
                    CcyPair = ccyPair,
                    Side = side,
                    OriginalAmount = amount
                };

                _testStateList.Add(testState);
            }
        }

        [Given(@"the market price is latest for all currencies")]
        public void GivenTheMarketPriceIsLatestForAllCurrencies()
        {
            foreach(TestState testState in _testStateList)
            {
                TestMarketDataProvider testMarketDataProvider = (TestMarketDataProvider)_dataProvider;
                testMarketDataProvider.SetLatest(testState.CcyPair);
            }
        }



        [When(@"we run the calculation")]
        public void WhenWeRunTheCalculation()
        {
            foreach(TestState testState in _testStateList)
            {
                CurrencyConversionResponse response = _currencyConverterManager.
                    GetCurrencyConversionDetails(testState.CcyPair, testState.Side == SideEnum.Buy, testState.OriginalAmount, testState.Guid);

                testState.ActualResponse = response;
            }
        }
        
        [Then(@"the expected results should be")]
        public void ThenTheExpectedResultsShouldBe(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid expectedGuid = Guid.Parse(row["Guid"]);
                ConversionEnum expectedConversionResult = (ConversionEnum)Enum.Parse(typeof(ConversionEnum), row["ConversionResult"]);
                string expectedConvertedAmountCurrency = GetDefaultString(row["ConvertedAmountCurrency"]);
                decimal? expectedConvertedAmount = GetDefaultDecimal(row["ConvertedAmount"]);
                decimal? expectedPxUsed = GetDefaultDecimal(row["PxUsed"]);
                string expectedCcyPair = GetDefaultString(row["CcyPair"]);
                decimal? expectedOriginalAmount = GetDefaultDecimal(row["OriginalAmount"]);
                string expectedOriginalAmountCcy = GetDefaultString(row["OriginalAmountCcy"]);
                SideEnum expectedSide = row["Side"] == "Buy" ? SideEnum.Buy : SideEnum.Sell;

                CurrencyConversionResponse actualResponse = _testStateList.Find(x => x.Id == id).ActualResponse;

                Assert.AreEqual(expectedConversionResult, actualResponse.ConversionResults);
                Assert.AreEqual(expectedConvertedAmountCurrency, actualResponse.ConvertedAmountCcy);
                Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
                Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
                Assert.AreEqual(expectedCcyPair, actualResponse.CcyPair);
                Assert.AreEqual(expectedOriginalAmountCcy, actualResponse.OriginalAmountCcy);
                Assert.AreEqual(expectedOriginalAmount, actualResponse.OriginalAmount);
                Assert.AreEqual(expectedSide, actualResponse.Side);
                Assert.AreEqual(expectedGuid, actualResponse.Id);

            }
        }


        [Then(@"database should store")]
        public void ThenDatabaseShouldStore(Table fxCurrencyConversionAudit)
        {
            foreach (TableRow row in fxCurrencyConversionAudit.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid expectedGuid = Guid.Parse(row["Guid"]);
                ConversionEnum expectedConversionResult = (ConversionEnum)Enum.Parse(typeof(ConversionEnum), row["ConversionResult"]);
                string expectedConvertedAmountCurrency = GetDefaultString(row["ConvertedAmountCurrency"]);
                decimal? expectedConvertedAmount = GetDefaultDecimal(row["ConvertedAmount"]);
                decimal? expectedPxUsed = GetDefaultDecimal(row["PxUsed"]);
                string expectedCcyPair = GetDefaultString(row["CcyPair"]);
                decimal? expectedOriginalAmount = GetDefaultDecimal(row["OriginalAmount"]);
                string expectedOriginalAmountCcy = GetDefaultString(row["OriginalAmountCcy"]);
                SideEnum expectedSide = row["Side"] == "Buy" ? SideEnum.Buy : SideEnum.Sell;

                //guid response mapping
                IList<CurrencyConversionResponse> actualResponseList = _testTradeRepositoryDb.GetFxCurrencyConversionAudit(expectedGuid);

                Assert.IsNotNull(actualResponseList);
                Assert.AreEqual(1, actualResponseList.Count);

                CurrencyConversionResponse actualResponse = actualResponseList[0];

                Assert.AreEqual(expectedConversionResult, actualResponse.ConversionResults);
                Assert.AreEqual(expectedConvertedAmountCurrency, actualResponse.ConvertedAmountCcy);
                Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
                Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
                Assert.AreEqual(expectedCcyPair, actualResponse.CcyPair);
                Assert.AreEqual(expectedOriginalAmountCcy, actualResponse.OriginalAmountCcy);
                Assert.AreEqual(expectedOriginalAmount, actualResponse.OriginalAmount);
                Assert.AreEqual(expectedSide, actualResponse.Side);
                Assert.AreEqual(expectedGuid, actualResponse.Id);
            }
        }


        private string GetDefaultString(string val)
        {
            if (string.IsNullOrEmpty(val))
                return null;
            return val;
        }

        private decimal? GetDefaultDecimal(string val)
        {
            if (string.IsNullOrEmpty(val))
                return null;
            return decimal.Parse(val);
        }
    }
}
