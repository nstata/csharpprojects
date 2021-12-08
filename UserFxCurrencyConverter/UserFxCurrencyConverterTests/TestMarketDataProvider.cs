using System;
using System.Collections.Generic;
using System.Linq;
using UserFxCurrencyConverter.Enums;
using UserFxCurrencyConverter.Interfaces;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverterIntegrationTests
{
    public class TestMarketDataProvider : IDataProvider
    {
        //private List<CurrencyPriceDetails> _currencyPriceDetails;
        private Dictionary<string, UserCurrencyPriceDetails> _currencyPriceDetails;

        public TestMarketDataProvider()
        {
            DateTime now = DateTime.Now;
            DateTime staleTime = now.AddMilliseconds(-15);

            _currencyPriceDetails = new Dictionary<string, UserCurrencyPriceDetails>()
            {
                {"AUD/CAD", GetCurrencyPriceDetail("AUD/CAD",0.91946m,0.9207m, now, UserMarketPriceStateEnum.MarketClosed) },
                {"AUD/USD", GetCurrencyPriceDetail("AUD/USD",0.73273m,0.73334m, staleTime) },
                {"EUR/CHF", GetCurrencyPriceDetail("EUR/CHF",1.0538m,1.055m, now) },
                {"EUR/GBP", GetCurrencyPriceDetail("EUR/GBP",0.85283m,0.85357m, now) },
                {"EUR/JPY", GetCurrencyPriceDetail("EUR/JPY",130.296m,130.446m, now) },
                {"EUR/USD", GetCurrencyPriceDetail("EUR/USD",1.1442m,1.14514m, now) },
                {"GBP/CAD", GetCurrencyPriceDetail("GBP/CAD",1.68345m,1.68495m, now) },
                {"GBP/USD", GetCurrencyPriceDetail("GBP/USD",1.34126m,1.34272m, now) },
                {"USD/CAD", GetCurrencyPriceDetail("USD/CAD",1.2545m,1.2555m, now) },
                {"USD/CHF", GetCurrencyPriceDetail("USD/CHF",0.92094m,0.92204m, now) },
                {"USD/JPY", GetCurrencyPriceDetail("USD/JPY",113.851m,113.932m, now) }
            };
        }

        public UserCurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair)
        {
            if (_currencyPriceDetails.TryGetValue(ccyPair, out UserCurrencyPriceDetails currencyPriceDetails))
            {
                return currencyPriceDetails;
            }

            return null;
        }

        private UserCurrencyPriceDetails GetCurrencyPriceDetail(string ccyPair, decimal bidPx, decimal askPx,
            DateTime priceDateTime,
            UserMarketPriceStateEnum priceState = UserMarketPriceStateEnum.MarketOpen)
        {
            return new UserCurrencyPriceDetails
            {
                CcyPair = ccyPair,
                BidPx = bidPx,
                AskPx = askPx,
                PriceState = priceState,
                LastUpdated = priceDateTime
            };
        }

        internal void SetLatest(string ccyPair)
        {
            if (_currencyPriceDetails.ContainsKey(ccyPair) == false)
            {
                return;
            }
            

            UserCurrencyPriceDetails currencyPriceDetails = _currencyPriceDetails[ccyPair];

            UserCurrencyPriceDetails currencyPriceDetails2 = new UserCurrencyPriceDetails
            {
                CcyPair = currencyPriceDetails.CcyPair,
                BidPx = currencyPriceDetails.BidPx,
                AskPx = currencyPriceDetails.AskPx,
                PriceState = currencyPriceDetails.PriceState,
                LastUpdated = DateTime.Now
            };

            _currencyPriceDetails[ccyPair] = currencyPriceDetails2;
        }

        public List<string> GetAllCurrencyPairs()
        {
            return _currencyPriceDetails.Keys.ToList();
        }
    }
}
