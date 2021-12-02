using UserFxCurrencyConverter.UserCurrencyConverter;
using UserFxCurrencyConverter.DataProvider;
using UserFxCurrencyConverter.DB;
using UserFxCurrencyConverter.Enums;
using FxCurrencyConverterIntegrationTests.DB;
using UserFxCurrencyConverterIntegrationTests.State;
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
        private readonly UserCurrencyConverterManager _currencyConverterManager;
        private readonly List<TestState> _testStateList = new List<TestState>();
        private readonly TestTradeRepositoryDb _testTradeRepositoryDb = new TestTradeRepositoryDb();

        public FxCurrencyConversionSteps()
        {
            _dataProvider = new TestMarketDataProvider();
            ITradeRepositoryDb tradeRepositoryDb = new TradeRepositoryDb();
            _currencyConverterManager = new UserCurrencyConverterManager(_dataProvider, tradeRepositoryDb);
        }


        [Given(@"the database is clean")]
        public void GivenTheDatabaseIsClean()
        {
            _testTradeRepositoryDb.CleanTable();
        }


        [Given(@"user has below settings:")]
        public void GivenUserHasBelowSettings(Table table)
        {
            
        }



        [Given(@"the request received is:")]
        public void GivenTheRequestReceivedIs(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid requestId = Guid.Parse(row["RequestId"]);
                long userId = long.Parse(row["UserId"]);
                string ccyPair = row["CurrencyPair"];
                UserSideEnum side = row["Side"] == "Buy" ? UserSideEnum.Buy : UserSideEnum.Sell;
                decimal amount = decimal.Parse(row["Amount"]);

                TestState testState = new TestState
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = side,
                    OriginalAmount = amount
                };

                _testStateList.Add(testState);
            }
        }



        public void SetLatestTimeForCcyPair(string ccyPair)
        {
            TestMarketDataProvider testMarketDataProvider = (TestMarketDataProvider)_dataProvider;
            testMarketDataProvider.SetLatest(ccyPair);
        }


        [When(@"we run the calculation with latest market price")]
        public void WhenWeRunTheCalculationWithLatestMarketPrice()
        {
            foreach (TestState testState in _testStateList)
            {
                SetLatestTimeForCcyPair(testState.CcyPair.ToUpper());
                UserCurrencyConversionResponse response = _currencyConverterManager.
                    GetCurrencyConversionDetailsForUser(testState.RequestId, testState.UserId, testState.CcyPair, testState.Side == UserSideEnum.Buy, testState.OriginalAmount);

                testState.ActualResponse = response;
            }
        }


        [When(@"we run the calculation")]
        public void WhenWeRunTheCalculation()
        {
            foreach (TestState testState in _testStateList)
            {
                UserCurrencyConversionResponse response = _currencyConverterManager.
                    GetCurrencyConversionDetailsForUser(testState.RequestId, testState.UserId, testState.CcyPair, testState.Side == UserSideEnum.Buy, testState.OriginalAmount);

                testState.ActualResponse = response;
            }
        }

        [Then(@"the expected results should be")]
        public void ThenTheExpectedResultsShouldBe(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid expectedRequestId = Guid.Parse(row["RequestId"]);
                long expectedUserId = long.Parse(row["UserId"]);
                UserConversionEnum expectedConversionResult = (UserConversionEnum)Enum.Parse(typeof(UserConversionEnum), row["ConversionResult"]);
                string expectedConvertedAmountCurrency = GetDefaultString(row["ConvertedAmountCurrency"]);
                decimal? expectedConvertedAmount = GetDefaultDecimal(row["ConvertedAmount"]);
                decimal? expectedPxUsed = GetDefaultDecimal(row["PxUsed"]);
                string expectedCcyPair = GetDefaultString(row["CcyPair"]);
                decimal? expectedOriginalAmount = GetDefaultDecimal(row["OriginalAmount"]);
                string expectedOriginalAmountCcy = GetDefaultString(row["OriginalAmountCcy"]);
                UserSideEnum expectedSide = row["Side"] == "Buy" ? UserSideEnum.Buy : UserSideEnum.Sell;

                UserCurrencyConversionResponse actualResponse = _testStateList.Find(x => x.Id == id).ActualResponse;

                Assert.AreEqual(expectedRequestId, actualResponse.RequestId);
                Assert.AreEqual(expectedUserId, actualResponse.UserId);
                Assert.AreEqual(expectedConversionResult, actualResponse.ConversionResults);
                Assert.AreEqual(expectedConvertedAmountCurrency, actualResponse.ConvertedAmountCcy);
                Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
                Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
                Assert.AreEqual(expectedCcyPair, actualResponse.CcyPair);
                Assert.AreEqual(expectedOriginalAmountCcy, actualResponse.OriginalAmountCcy);
                Assert.AreEqual(expectedOriginalAmount, actualResponse.OriginalAmount);
                Assert.AreEqual(expectedSide, actualResponse.Side);
                


            }
        }


        [Then(@"database should store")]
        public void ThenDatabaseShouldStore(Table fxCurrencyConversionAudit)
        {
            foreach (TableRow row in fxCurrencyConversionAudit.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid expectedRequestId = Guid.Parse(row["RequestId"]);
                long expectedUserId = long.Parse(row["UserId"]);
                UserConversionEnum expectedConversionResult = (UserConversionEnum)Enum.Parse(typeof(UserConversionEnum), row["ConversionResult"]);
                string expectedConvertedAmountCurrency = GetDefaultString(row["ConvertedAmountCurrency"]);
                decimal? expectedConvertedAmount = GetDefaultDecimal(row["ConvertedAmount"]);
                decimal? expectedPxUsed = GetDefaultDecimal(row["PxUsed"]);
                string expectedCcyPair = GetDefaultString(row["CcyPair"]);
                decimal? expectedOriginalAmount = GetDefaultDecimal(row["OriginalAmount"]);
                string expectedOriginalAmountCcy = GetDefaultString(row["OriginalAmountCcy"]);
                UserSideEnum expectedSide = row["Side"] == "Buy" ? UserSideEnum.Buy : UserSideEnum.Sell;

                //guid response mapping
                IList<UserCurrencyConversionResponse> actualResponseList = _testTradeRepositoryDb.GetFxCurrencyConversionAudit(expectedRequestId);

                Assert.IsNotNull(actualResponseList);
                Assert.AreEqual(1, actualResponseList.Count);

                UserCurrencyConversionResponse actualResponse = actualResponseList[0];

                Assert.AreEqual(expectedRequestId, actualResponse.RequestId);
                Assert.AreEqual(expectedUserId, actualResponse.UserId);
                Assert.AreEqual(expectedConversionResult, actualResponse.ConversionResults);
                Assert.AreEqual(expectedConvertedAmountCurrency, actualResponse.ConvertedAmountCcy);
                Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
                Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
                Assert.AreEqual(expectedCcyPair, actualResponse.CcyPair);
                Assert.AreEqual(expectedOriginalAmountCcy, actualResponse.OriginalAmountCcy);
                Assert.AreEqual(expectedOriginalAmount, actualResponse.OriginalAmount);
                Assert.AreEqual(expectedSide, actualResponse.Side);
               
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
