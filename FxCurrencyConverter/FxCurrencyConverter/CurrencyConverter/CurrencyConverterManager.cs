using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.Enums;
using System;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConverterManager
    {
        private readonly IDataProvider _marketDataProvider;
            

        public CurrencyConverterManager(IDataProvider marketDataProvider)
        {
            _marketDataProvider = marketDataProvider;
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


        public CurrencyConversionResponse GetCurrencyConversionDetails(string ccyPair, bool isBuy, decimal amount)
        {
            // checks
            CurrencyConversionResponse invalidData = CheckIfTheInputDataIsInvalid( ccyPair, isBuy, amount);
            if (invalidData != null)
            {
                return invalidData;
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

            // scenario 1: direct conversion exists between baseCcy/quotedCcy
            CurrencyPriceDetails ccyPriceDetails = _marketDataProvider.GetCurrencyPriceDetails(ccyPair);

            if (ccyPriceDetails != null)
            {
                if(ccyPriceDetails.PriceState == MarketPriceStateEnum.MarketClosed)
                {
                    return new CurrencyConversionResponse
                    {
                        CcyPair = ccyPair,
                        Side = isBuy ? SideEnum.Buy : SideEnum.Sell,
                        OriginalAmount = amount,
                        ConversionResults = ConversionEnum.MarketClosed,
                    };
                }

                if ((DateTime.Now - ccyPriceDetails.LastUpdated).TotalMilliseconds > 10)
                {
                    return new CurrencyConversionResponse
                    {
                        CcyPair = ccyPair,
                        Side = isBuy ? SideEnum.Buy : SideEnum.Sell,
                        OriginalAmount = amount,
                        ConversionResults = ConversionEnum.StalePrice,
                    };
                }


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

        private CurrencyConversionResponse CheckIfTheInputDataIsInvalid(string ccyPair, bool isBuy, decimal amount)
        {
            // checks
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

             return null;
        }
    }
}
