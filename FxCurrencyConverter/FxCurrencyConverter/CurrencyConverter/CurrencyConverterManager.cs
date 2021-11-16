using FxCurrencyConverter.DataProvider;
using System.Collections.Generic;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConverterManager
    {
        private readonly List<CurrencyPriceDetails> _currencyPriceDetails = new();

        public CurrencyConverterManager(IDataProvider dataProvider)
        {
            _currencyPriceDetails = dataProvider.GetCurrencyPriceDetails();
        }




        public IEnumerable<CurrencyPriceDetails> GetCurrencyPrices()
        {
            return _currencyPriceDetails;
        }


        public CurrencyConversionResponse GetCurrencyConversionDetails(string ccyPair, bool isBuy, decimal amount)
        {
            string[] tokens;
            if (ccyPair.Contains("%2F"))
            {
                tokens = ccyPair.Split("%2F");
            }
            else
            {
                tokens = ccyPair.Split("/");
            }

            string baseCcy = tokens[0];
            string quotedCcy = tokens[1];

            // scenario 1: direct conversion exists between baseCcy/quotedCcy
            string ccyToCheck = GetCcyPair(baseCcy, quotedCcy);
            CurrencyPriceDetails ccyPriceDetails = _currencyPriceDetails.Find(ccyPrice => ccyPrice.CcyPair == ccyToCheck);

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
                    CcyPair = ccyToCheck,
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
                CcyPair = ccyToCheck,
                Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                OriginalAmount = amount,
                OriginalAmountCcy = baseCcy,
                ConversionResults = Enums.ConversionEnum.ConversionFailedForCcyPair,
            };
        }


        private string GetCcyPair(string ccy1, string ccy2) => $"{ccy1}/{ccy2}";
    }
}
