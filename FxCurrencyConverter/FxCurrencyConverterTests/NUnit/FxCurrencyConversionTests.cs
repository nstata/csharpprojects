using FxCurrencyConverter.CurrencyConverter;
using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.Enums;
using NUnit.Framework;
using System;

namespace FxCurrencyConverterNunitTests
{
    [TestFixture]
    public class FxCurrencyConversionTests
    {
        private CurrencyConversionResponse NewMethod(string inputCurrencyPairs, bool isBuy, decimal amount)
        {
            // init
            IDataProvider dataProvider = new TestMarketDataProvider();

            CurrencyConverterManager currencyConverterManager =
                new CurrencyConverterManager(dataProvider, null);

            // execute / run
            CurrencyConversionResponse actualResponse = currencyConverterManager.
                ConvertCurrency(inputCurrencyPairs, isBuy, amount, Guid.NewGuid());

            return actualResponse;
        }

        [TestCase("GBP/USA", true, 100)]
        [TestCase("GBP/", true, 100)]
        [TestCase("GBP/GBP", true, 100)]
        [TestCase("XXX/YYY", true, 100)]
        [TestCase(null, true, 100)]
        public void WhenUsingInvalidCurrencyPairConversionShouldNotSucceed(string inputCurrencyPairs, bool isBuy, decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(ConversionEnum.ConversionFailedInvalidCcyPair, actualResponse.ConversionResults);
            Assert.AreEqual(null, actualResponse.ConvertedAmountCcy);
            Assert.AreEqual(null, actualResponse.ConvertedAmount);
            Assert.AreEqual(null, actualResponse.PxUsed);
            
        }



        [TestCase("GBP/USD", true, 0)]
        [TestCase("GBP/USD", true, -10)]
        [TestCase("GBP/USD", false, 0)]
        [TestCase("GBP/USD", false, -10)]
        public void WhenTheAmountEnteredIsInvalidConversionShouldNotSucceed(string inputCurrencyPairs, bool isBuy, decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(ConversionEnum.ConversionFailedInvalidAmount, actualResponse.ConversionResults);
            Assert.AreEqual(null, actualResponse.ConvertedAmountCcy);
            Assert.AreEqual(null, actualResponse.ConvertedAmount);
            Assert.AreEqual(null, actualResponse.PxUsed);
        }



        [TestCase("GBP/USD", true, 100, 1.34272)]
        public void WhenBuyingAValidCurrencyVerifyThatAskPriceShouldBeUsedToCalculateTheConvertedAmount(string inputCurrencyPairs, bool isBuy, decimal amount, decimal expectedPxUsed)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
            Assert.AreEqual(inputCurrencyPairs, actualResponse.CcyPair);

        }



        [TestCase("GBP/USD", true, 100)]
        public void WhenBuyingAValidCurrencyVerifyThatBuyIsDisplayedInTheResponseBody(string inputCurrencyPairs, bool isBuy, decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            SideEnum expectedSide;
            if (isBuy == true)
                expectedSide = SideEnum.Buy;
            else
                expectedSide = SideEnum.Sell;


            // assert
            Assert.AreEqual(expectedSide, actualResponse.Side);
         
        }



        [TestCase("GBP/USD", true, 100, 134.272)]
        public void WhenBuyingAValidCurrencyConvertedAmountShouldBeCorrectlyCalculatedAndDisplayednTheResponseBody(string inputCurrencyPairs, bool isBuy, decimal amount, decimal expectedConvertedAmount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);

        }



        [TestCase("GBP/USD", false, 100, 1.34126)]
        public void WhenSellingAValidCurrencyVerifyThatBidPriceShouldBeUsedToCalculateTheConvertedAmount(string inputCurrencyPairs, bool isBuy, decimal amount, decimal expectedPxUsed)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);   

        }



        [TestCase("GBP/USD", false, 100)]
        public void WhenSellingAValidCurrencyVerifyThatSellIsDisplayedInTheResponseBody(string inputCurrencyPairs, bool isBuy, decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            SideEnum expectedSide;
            if (isBuy == true)
                expectedSide = SideEnum.Buy;
            else
                expectedSide = SideEnum.Sell;

            // assert
            Assert.AreEqual(expectedSide, actualResponse.Side);

        }



        [TestCase("GBP/USD", false, 100, 134.126)]
        public void WhenSellingAValidCurrencyConvertedAmountShouldBeCorrectlyCalculatedAndDisplayednTheResponseBody(string inputCurrencyPairs, bool isBuy, decimal amount, decimal expectedConvertedAmount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);


            // assert
            Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);

        }



        [TestCase("gbp/usd", false, 100, "GBP/USD", 134.126, 1.34126)]
        [TestCase("Gbp/usd", false, 100, "GBP/USD", 134.126, 1.34126)]
        [TestCase("GBP/Usd", false, 100, "GBP/USD", 134.126, 1.34126)]
        [TestCase("gbp/USD", false, 100, "GBP/USD", 134.126, 1.34126)]
        public void WhenTheInputCurrencyPairIsInAllLowercaseOrMixedUppercaseLowerCaseConversionShouldBeSuccessful(string inputCurrencyPairs, bool isBuy,
            decimal amount, string expectedCcyPair, decimal expectedConvertedAmount, decimal expectedPxUsed)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(expectedConvertedAmount, actualResponse.ConvertedAmount);
            Assert.AreEqual(expectedPxUsed, actualResponse.PxUsed);
            Assert.AreEqual(expectedCcyPair, actualResponse.CcyPair);
            string expectedConvertedAmountCcy = expectedCcyPair.Substring(4, 3);
            Assert.AreEqual(expectedConvertedAmountCcy, actualResponse.ConvertedAmountCcy);   
            
        }



        [TestCase("AUD/CAD", true, 10)]
        public void WhenTheCurrencyPairIsValidAndMarketIsClosed(string inputCurrencyPairs, bool isBuy,
            decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(ConversionEnum.MarketClosed, actualResponse.ConversionResults);
            Assert.AreEqual(null, actualResponse.ConvertedAmountCcy);
            Assert.AreEqual(null, actualResponse.ConvertedAmount);
            Assert.AreEqual(null, actualResponse.PxUsed);

        }



        [TestCase("AUD/USD", true, 10)]
        public void WhenTheCurrencyPairIsValidAndPriceIsStale(string inputCurrencyPairs, bool isBuy,
            decimal amount)
        {
            // init and execute / run
            CurrencyConversionResponse actualResponse = NewMethod(inputCurrencyPairs, isBuy, amount);

            // assert
            Assert.AreEqual(ConversionEnum.StalePrice, actualResponse.ConversionResults);
            Assert.AreEqual(null, actualResponse.ConvertedAmountCcy);
            Assert.AreEqual(null, actualResponse.ConvertedAmount);
            Assert.AreEqual(null, actualResponse.PxUsed);

        }

    }
}
