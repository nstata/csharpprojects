using System;
using UserFxCurrencyConverter.DataProvider;
using UserFxCurrencyConverter.DB;
using UserFxCurrencyConverter.Enums;

namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserCurrencyConverterManager
    {
        private readonly IDataProvider _marketDataProvider;
        private readonly ITradeRepositoryDb _tradeRepositoryDb;

        public UserCurrencyConverterManager(IDataProvider marketDataProvider, ITradeRepositoryDb tradeRepositoryDb)
        {
            _marketDataProvider = marketDataProvider;
            _tradeRepositoryDb = tradeRepositoryDb;
        }


        private UserCurrencyConversionResponse ConvertCurrency(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount)
        {
            // checks for invalid data
            UserCurrencyConversionResponse invalidData = CheckIfTheInputDataIsInvalid(requestId, userId, ccyPair, isBuy, amount);
            if (invalidData != null)
            {
                return invalidData;
            }

            // duplicate check
            bool isDuplicate = _tradeRepositoryDb.IsDuplicateRequest(requestId, userId);
            if (isDuplicate)
            {
                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = Enums.UserConversionEnum.DuplicateRequest,
                };
            }


            // check if user can trade


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
            UserCurrencyPriceDetails ccyPriceDetails = _marketDataProvider.GetCurrencyPriceDetails(ccyPair);

           // UserCurrencyConversionResponse response;
            if (ccyPriceDetails != null)
            {
                if (ccyPriceDetails.PriceState == UserMarketPriceStateEnum.MarketClosed)
                {
                    return new UserCurrencyConversionResponse
                    {
                        RequestId = requestId,
                        CcyPair = ccyPair,
                        Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                        OriginalAmount = amount,
                        ConversionResults = UserConversionEnum.MarketClosed,
                    };
                }

                if ((DateTime.Now - ccyPriceDetails.LastUpdated).TotalMilliseconds > 10)
                {
                    return new UserCurrencyConversionResponse
                    {
                        RequestId = requestId,
                        CcyPair = ccyPair,
                        Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                        OriginalAmount = amount,
                        ConversionResults = UserConversionEnum.StalePrice,
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

                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    OriginalAmountCcy = baseCcy,
                    ConvertedAmount = amount * pxUsed,
                    ConvertedAmountCcy = quotedCcy,
                    PxUsed = pxUsed,
                    ConversionResults = Enums.UserConversionEnum.Successful,
                };
            }


            // TODO
            // scenario 2: intermediate conversion exists between baseCcy/quotedCcy: baseCcy/otherCcy -> otherCcy/quotedCcy

            // scenario 3: no currency pair found to convert
            return new UserCurrencyConversionResponse
            {
                RequestId = requestId,
                CcyPair = ccyPair,
                Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                OriginalAmount = amount,
                ConversionResults = Enums.UserConversionEnum.ConversionFailedInvalidCcyPair,
            };
        }

        public UserCurrencyConversionResponse GetCurrencyConversionDetailsForUser(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount)
        {
            // log this in DB
            _tradeRepositoryDb.InsertIntoFxCurrencyConversionAudit(requestId, userId, ccyPair, isBuy, amount);

            UserCurrencyConversionResponse response = ConvertCurrency(requestId, userId, ccyPair, isBuy, amount);

            // update in DB
            _tradeRepositoryDb.UpdateFxCurrencyConversionAudit(response);

            return response;
        }

        private UserCurrencyConversionResponse CheckIfTheInputDataIsInvalid(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount)
        {
            // checks
            if (ccyPair == null)
            {
                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = UserConversionEnum.ConversionFailedInvalidCcyPair,
                };
            }

            if (amount <= 0)
            {
                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = UserConversionEnum.ConversionFailedInvalidAmount,
                };
            }

            if (requestId == Guid.Empty)
            {
                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = UserConversionEnum.ConversionFailedInvalidRequestId,
                };
            }

            if (userId <= 0)
            {
                return new UserCurrencyConversionResponse
                {
                    RequestId = requestId,
                    UserId = userId,
                    CcyPair = ccyPair,
                    Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                    OriginalAmount = amount,
                    ConversionResults = UserConversionEnum.ConversionFailedInvalidUserId,
                };
            }

            return null;
        }
    }
}
