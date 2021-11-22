using FxCurrencyConverter.CurrencyConverter;
using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.Enums;
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
        private readonly CurrencyConverterManager _currencyConverterManager;

        private readonly List<TestState> _testStateList = new List<TestState>();

        public FxCurrencyConversionSteps()
        {
            IDataProvider dataProvider = new HardCodedValuesDataProvider();
            _currencyConverterManager = new CurrencyConverterManager(dataProvider);
        }


        [Given(@"our input is:")]
        public void GivenOurInputIs(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
                string ccyPair = row["CurrencyPair"];
                SideEnum side = row["Side"] == "Buy" ? SideEnum.Buy : SideEnum.Sell;
                decimal amount = decimal.Parse(row["Amount"]);
               

                TestState testState = new TestState
                {
                    Id = id,
                    CcyPair = ccyPair,
                    Side = side,
                    OriginalAmount = amount
                };

                _testStateList.Add(testState);
            }
        }
        
        [When(@"we run the calculation")]
        public void WhenWeRunTheCalculation()
        {
            foreach(TestState testState in _testStateList)
            {
                CurrencyConversionResponse response = _currencyConverterManager.
                    GetCurrencyConversionDetails(testState.CcyPair, testState.Side == SideEnum.Buy, testState.OriginalAmount);

                testState.ActualResponse = response;
            }
        }
        
        [Then(@"the expected results should be")]
        public void ThenTheExpectedResultsShouldBe(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                int id = int.Parse(row["Id"]);
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
