using FxCurrencyConverter.CurrencyConverter;
using System.Collections.Generic;

namespace FxCurrencyConverter.DataProvider
{
    public class HardCodedValuesDataProvider : IDataProvider
    {
        private List<CurrencyPriceDetails> _currencyPriceDetails;

        public HardCodedValuesDataProvider()
        {
            _currencyPriceDetails = new List<CurrencyPriceDetails>()
            {
                GetCurrencyPriceDetail("AUD/CAD",0.91946m,0.9207m),
                GetCurrencyPriceDetail("AUD/USD",0.73273m,0.73334m),
                GetCurrencyPriceDetail("EUR/CHF",1.0538m,1.055m),
                GetCurrencyPriceDetail("EUR/GBP",0.85283m,0.85357m),
                GetCurrencyPriceDetail("EUR/JPY",130.296m,130.446m),
                GetCurrencyPriceDetail("EUR/USD",1.1442m,1.14514m),
                GetCurrencyPriceDetail("GBP/CAD",1.68345m,1.68495m),
                GetCurrencyPriceDetail("GBP/USD",1.34126m,1.34272m),
                GetCurrencyPriceDetail("USD/CAD",1.2545m,1.2555m),
                GetCurrencyPriceDetail("USD/CHF",0.92094m,0.92204m),
                GetCurrencyPriceDetail("USD/JPY",113.851m,113.932m),
            };
        }

        public CurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair)
        {
            return _currencyPriceDetails.Find(x => x.CcyPair == ccyPair);
        }

        private CurrencyPriceDetails GetCurrencyPriceDetail(string ccyPair, decimal bidPx, decimal askPx)
        {
            return new CurrencyPriceDetails
            {
                CcyPair = ccyPair,
                BidPx = bidPx,
                AskPx = askPx
            };
        }
    }
}
