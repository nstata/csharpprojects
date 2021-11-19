using FxCurrencyConverter.DataProvider;
using System.Collections.Generic;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConverterManager
    {
        private readonly List<CurrencyPriceDetails> _currencyPriceDetails;

        public CurrencyConverterManager()
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

        private CurrencyPriceDetails GetCurrencyPriceDetail(string ccyPair, decimal bidPx, decimal askPx)
        {
            return new CurrencyPriceDetails
            {
                CcyPair = ccyPair,
                BidPx = bidPx,
                AskPx = askPx
            };
        }


        public IEnumerable<CurrencyPriceDetails> GetCurrencyPrices()
        {
            return _currencyPriceDetails;
        }


        public CurrencyConversionResponse GetCurrencyConversionDetails(string ccyPair, bool isBuy, decimal amount)
        {
            if (ccyPair == null)
            {
                return new CurrencyConversionResponse
                {
                    CcyPair = ccyPair,
                    Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidCcyPair,
                };
            }

            ccyPair = ccyPair.ToUpper();

            string[] tokens;
            if (ccyPair.Contains("%2F"))
            {
                ccyPair = ccyPair.Replace("%2F", "/");
            }

            tokens = ccyPair.Split("/");

            string baseCcy = tokens[0];
            string quotedCcy = tokens[1];

            // checks
            if (amount <= 0)
            {
                return new CurrencyConversionResponse
                {
                    CcyPair = ccyPair,
                    Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidAmount,
                };
            }


            // scenario 1: direct conversion exists between baseCcy/quotedCcy
            CurrencyPriceDetails ccyPriceDetails = _currencyPriceDetails.Find(ccyPrice => ccyPrice.CcyPair == ccyPair);

            if (ccyPriceDetails != null)
            {
                decimal pxUsed;
                if (isBuy)
                {
                    // exchange would be selling
                    pxUsed = ccyPriceDetails.AskPx;
                }
                else
                {
                    // exchange would be buying
                    pxUsed = ccyPriceDetails.BidPx;
                }

                return new CurrencyConversionResponse
                {
                    CcyPair = ccyPair,
                    Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                    OriginalAmount = amount,
                    OriginalAmountCcy = baseCcy,
                    ConvertedAmount = amount * pxUsed,
                    ConvertedAmountCcy = quotedCcy,
                    PxUsed = pxUsed,
                    ConversionResults = Enums.ConversionEnum.Successful,
                };
            }


            // TODO
            // scenario 2: intermediate conversion exists between baseCcy/quotedCcy: baseCcy/otherCcy -> otherCcy/quotedCcy


            // scenario 3: no currency pair found to convert
            return new CurrencyConversionResponse
            {
                CcyPair = ccyPair,
                Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                OriginalAmount = amount,
                ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidCcyPair,
            };
        }


        private string GetCcyPair(string ccy1, string ccy2) => $"{ccy1}/{ccy2}";
    }
}
