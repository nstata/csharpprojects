using FxCurrencyConverter.DataProvider;
using FxCurrencyConverter.DB;
using FxCurrencyConverter.Enums;
using System;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConverterManager
    {
        private readonly IDataProvider _marketDataProvider;
        private readonly ITradeRepositoryDb _tradeRepositoryDb;

        public CurrencyConverterManager(IDataProvider marketDataProvider, ITradeRepositoryDb tradeRepositoryDb)
        {
            _marketDataProvider = marketDataProvider;
            _tradeRepositoryDb = tradeRepositoryDb;
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

        public CurrencyConversionResponse ConvertCurrency(string ccyPair, bool isBuy, decimal amount, Guid id)
        {
            // checks
            CurrencyConversionResponse invalidData = CheckIfTheInputDataIsInvalid(ccyPair, isBuy, amount, id);
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

            CurrencyConversionResponse response;
            if (ccyPriceDetails != null)
            {
                if (ccyPriceDetails.PriceState == MarketPriceStateEnum.MarketClosed)
                {
                    return new CurrencyConversionResponse
                    {
                        Id = id,
                        CcyPair = ccyPair,
                        Side = isBuy ? SideEnum.Buy : SideEnum.Sell,
                        OriginalAmount = amount,
                        ConversionResults = ConversionEnum.MarketClosed,
                    };
                }

                DateTime now = DateTime.Now;
                if ((now - ccyPriceDetails.LastUpdated).TotalMilliseconds > 10)
                {
                    return new CurrencyConversionResponse
                    {
                        Id = id,
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
                    Id = id,
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
                Id = id,
                CcyPair = ccyPair,
                Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                OriginalAmount = amount,
                ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidCcyPair,
            };
        }

        public CurrencyConversionResponse GetCurrencyConversionDetails(string ccyPair, bool isBuy, decimal amount, Guid id)
        {
            CurrencyConversionResponse response = ConvertCurrency( ccyPair, isBuy, amount, id);
            _tradeRepositoryDb.InsertIntoFxCurrencyConversionAudit(response);
            return response;
        }

        private CurrencyConversionResponse CheckIfTheInputDataIsInvalid(string ccyPair, bool isBuy, decimal amount, Guid id)
        {
            // checks
            if (ccyPair == null)
            {
                CurrencyConversionResponse response = new CurrencyConversionResponse
                {
                    Id = id,
                    CcyPair = ccyPair,
                    Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidCcyPair,
                };
                return response;
            }

            if (amount <= 0)
            {
                CurrencyConversionResponse response = new CurrencyConversionResponse
                {
                    Id = id,
                    CcyPair = ccyPair,
                    Side = isBuy ? Enums.SideEnum.Buy : Enums.SideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = Enums.ConversionEnum.ConversionFailedInvalidAmount,
                };
                return response;
            }

            return null;
        }
    }
}
