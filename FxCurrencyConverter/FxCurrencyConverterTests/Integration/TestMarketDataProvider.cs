using FxCurrencyConverter.CurrencyConverter;
using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.Enums;
using System.Collections.Generic;
using System;

namespace FxCurrencyConverterIntegrationTests
{
    public class TestMarketDataProvider : IDataProvider
    {
        private List<CurrencyPriceDetails> _currencyPriceDetails;

        public TestMarketDataProvider()
        {
            DateTime now = DateTime.Now;
            DateTime staleTime = now.AddMilliseconds(-15);

            _currencyPriceDetails = new List<CurrencyPriceDetails>()
            {
                GetCurrencyPriceDetail("AUD/CAD",0.91946m,0.9207m, now, MarketPriceStateEnum.MarketClosed),
                GetCurrencyPriceDetail("AUD/USD",0.73273m,0.73334m, staleTime),
                GetCurrencyPriceDetail("EUR/CHF",1.0538m,1.055m, now),
                GetCurrencyPriceDetail("EUR/GBP",0.85283m,0.85357m, now),
                GetCurrencyPriceDetail("EUR/JPY",130.296m,130.446m, now),
                GetCurrencyPriceDetail("EUR/USD",1.1442m,1.14514m, now),
                GetCurrencyPriceDetail("GBP/CAD",1.68345m,1.68495m, now),
                GetCurrencyPriceDetail("GBP/USD",1.34126m,1.34272m, now),
                GetCurrencyPriceDetail("USD/CAD",1.2545m,1.2555m, now),
                GetCurrencyPriceDetail("USD/CHF",0.92094m,0.92204m, now),
                GetCurrencyPriceDetail("USD/JPY",113.851m,113.932m, now),
            };
        }

        public CurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair)
        {
            return _currencyPriceDetails.Find(x => x.CcyPair == ccyPair);
        }

        private CurrencyPriceDetails GetCurrencyPriceDetail(string ccyPair, decimal bidPx, decimal askPx,
            DateTime priceDateTime,
            MarketPriceStateEnum priceState = MarketPriceStateEnum.MarketOpen)
        {
            return new CurrencyPriceDetails
            {
                CcyPair = ccyPair,
                BidPx = bidPx,
                AskPx = askPx,
                PriceState = priceState,
                LastUpdated = priceDateTime
            };
        }
    }
}
