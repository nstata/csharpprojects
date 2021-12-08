using FxCurrencyConverterIntegrationTests.DB;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;
using UserFxCurrencyConverter.DB;
using UserFxCurrencyConverter.Enums;
using UserFxCurrencyConverter.Interfaces;
using UserFxCurrencyConverter.UserCurrencyConverter;
using UserFxCurrencyConverterIntegrationTests.State;

namespace UserFxCurrencyConverterIntegrationTests.Steps
{
    [Binding]
    public class UserFxCurrencyConversionSteps

    {
        private readonly IDataProvider _dataProvider;
        private readonly IUserSettings _userSettingsProvider;
        private readonly UserCurrencyConverterManager _currencyConverterManager;
        private readonly List<TestState> _testStateList = new List<TestState>();
        private readonly TestTradeRepositoryDb _testTradeRepositoryDb = new TestTradeRepositoryDb();

        public UserFxCurrencyConversionSteps()
        {
            _dataProvider = new TestMarketDataProvider();
            _userSettingsProvider = new TestUserSettingsProvider();
            ITradeRepositoryDb tradeRepositoryDb = new TradeRepositoryDb();
            _currencyConverterManager = new UserCurrencyConverterManager(_dataProvider, tradeRepositoryDb, _userSettingsProvider);
        }


        [Given(@"the database is clean")]
        public void GivenTheDatabaseIsClean()
        {
            _testTradeRepositoryDb.CleanTable();
        }


        [Given(@"we already have below rows in database:")]
        public void GivenWeAlreadyHaveBelowRowsInDatabase(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                Guid requestId = Guid.Parse(row["RequestId"]);
                long userId = long.Parse(row["UserId"]);
                string ccyPair = row["CcyPair"];
                bool isBuy = row["Side"] == "Buy" ? true : false;
                decimal amount = decimal.Parse(row["OriginalAmount"]);
                int conversionResultId = (int)Enum.Parse<UserConversionEnum>(row["ConversionResult"]);

                _testTradeRepositoryDb.InsertIntoFxCurrencyConversionAudit(requestId, userId, ccyPair, isBuy, amount, conversionResultId);
            }
        }


        [Given(@"user has below settings:")]
        public void GivenUserHasBelowSettings(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid requestId = Guid.Parse(row["RequestId"]);
                long userId = long.Parse(row["UserId"]);
                int conversionResultId = (int)Enum.Parse<UserConversionEnum>(row["ConversionResult"]);

                TestState testState = new TestState
                {
                    Id = id,
                    RequestId = requestId,
                    UserId = userId,
                  
                };

                _testStateList.Add(testState);
            }

        }


        [Given(@"the request received is:")]
        public void GivenTheRequestReceivedIs(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                Guid requestId = Guid.Parse(row["RequestId"]);
                long userId = long.Parse(row["UserId"]);
                string ccyPair = row["CcyPair"];
                UserSideEnum side = row["Side"] == "Buy" ? UserSideEnum.Buy : UserSideEnum.Sell;
                decimal amount = decimal.Parse(row["Amount"]);

                TestState testState = new TestState
                {
                    Id = id,
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair == "null" ? null : ccyPair,
                    Side = side,
                    OriginalAmount = amount
                };

                _testStateList.Add(testState);
            }
        }



        public void SetLatestTimeForCcyPair(string ccyPair)
        {
            if (ccyPair == null)
            {
                return;
            }

            TestMarketDataProvider testMarketDataProvider = (TestMarketDataProvider)_dataProvider;
            testMarketDataProvider.SetLatest(ccyPair.ToUpper());
        }


        [When(@"we run the calculation with latest market price")]
        public void WhenWeRunTheCalculationWithLatestMarketPrice()
        {
            foreach (TestState testState in _testStateList)
            {
                SetLatestTimeForCcyPair(testState.CcyPair);
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
                IList<UserCurrencyConversionResponse> actualResponseList = _testTradeRepositoryDb.GetFxCurrencyConversionAudit(expectedRequestId, expectedUserId);

                Assert.IsNotNull(actualResponseList);
                
                //TODO: figure out what to do here
                //Assert.AreEqual(1, actualResponseList.Count);

                UserCurrencyConversionResponse actualResponse = actualResponseList[0];

                Assert.AreEqual(expectedRequestId, actualResponse.RequestId);
                Assert.AreEqual(expectedUserId, actualResponse.UserId);
                Assert.AreEqual(expectedConversionResult, actualResponse.ConversionResults);
                Assert.AreEqual(expectedConvertedAmountCurrency, actualResponse.ConvertedAmountCcy);
                Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
                Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
                Assert.AreEqual(expectedCcyPair, GetDefaultString(actualResponse.CcyPair));
                Assert.AreEqual(expectedOriginalAmountCcy, GetDefaultString(actualResponse.OriginalAmountCcy));
                Assert.AreEqual(expectedOriginalAmount, actualResponse.OriginalAmount);
                Assert.AreEqual(expectedSide, actualResponse.Side);
               
            }
        }


        [Then(@"user settings are not called")]
        public void ThenUserSettingsAreNotCalled()
        {
            foreach(TestState testState in _testStateList)
            {
                long userId = testState.UserId;
                TestUserSettingsProvider settings = _userSettingsProvider as TestUserSettingsProvider;
                Assert.IsFalse(settings.UserSettingsCalled.ContainsKey(userId), "Expected UserSettings to be NOT called");
            }
        }



        private string GetDefaultString(string val)
        {
            if (val == null || val == "null")
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
