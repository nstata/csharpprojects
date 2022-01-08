using System;
using System.Collections.Generic;
using UserFxCurrencyConverter.DB;
using UserFxCurrencyConverter.Enums;
using UserFxCurrencyConverter.Interfaces;

namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserCurrencyConverterManager
    {
        private readonly IDataProvider _marketDataProvider;
        private readonly IUserSettings _userSettingsProvider;
        private readonly ITradeRepositoryDb _tradeRepositoryDb;
        private readonly List<string> _allCurrencyPairs;

        public UserCurrencyConverterManager(IDataProvider marketDataProvider, ITradeRepositoryDb tradeRepositoryDb, 
            IUserSettings userSettingsProvider)
        {
            _marketDataProvider = marketDataProvider;
            _tradeRepositoryDb = tradeRepositoryDb;
            _userSettingsProvider = userSettingsProvider;

            _allCurrencyPairs = marketDataProvider.GetAllCurrencyPairs();
        }

        public UserCurrencyConversionResponse GetCurrencyConversionDetailsForUser(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount)
        {
            // log this in DB
            int id = _tradeRepositoryDb.InsertIntoFxCurrencyConversionAudit(requestId, userId, ccyPair, isBuy, amount);

            UserCurrencyConversionResponse response = ConvertCurrency(requestId, userId, ccyPair, isBuy, amount, id);

            // update in DB
            _tradeRepositoryDb.UpdateFxCurrencyConversionAudit(response);

            return response;
        }


        private UserCurrencyConversionResponse GetUserCurrencyConversionResponse(Guid requestId, long userId, string ccyPair, 
            bool isBuy, decimal amount, int id, UserConversionEnum conversionResults, string baseCcy = null, decimal? pxUsed = null,
            string quotedCcy = null)
        {
            return new UserCurrencyConversionResponse
            {
                ID = id,
                RequestId = requestId,
                UserId = userId,
                CcyPair = ccyPair,
                Side = isBuy ? UserSideEnum.Buy : UserSideEnum.Sell,
                OriginalAmount = amount,
                ConversionResults = conversionResults,

                OriginalAmountCcy = baseCcy,
                ConvertedAmount = amount * pxUsed,
                ConvertedAmountCcy = quotedCcy,
                PxUsed = pxUsed,
            };
        }


        private UserCurrencyConversionResponse ConvertCurrency(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount, int id)
        {
            // checks for invalid request
            UserCurrencyConversionResponse invalidData = CheckIfTheInputDataIsInvalid(requestId, userId, ccyPair, isBuy, amount, id);
            if (invalidData != null)
            {
                return invalidData;
            }

            // duplicate check
            bool isDuplicate = _tradeRepositoryDb.IsDuplicateRequest(requestId, userId);
            if (isDuplicate)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.DuplicateRequest);                    
            }

            // check if user can trade
            invalidData = CheckUserSettings( userId, out UserSettings userSettings, requestId, ccyPair, isBuy, amount, id);
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
            UserCurrencyPriceDetails ccyPriceDetails = _marketDataProvider.GetCurrencyPriceDetails(ccyPair);


            // UserCurrencyConversionResponse response;
            if (ccyPriceDetails != null)
            {
                if (ccyPriceDetails.PriceState == UserMarketPriceStateEnum.MarketClosed)
                {
                    return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.DuplicateRequest);
                }

                if ((DateTime.Now - ccyPriceDetails.LastUpdated).TotalMilliseconds > 10)
                {
                    return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.DuplicateRequest);
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

                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.DuplicateRequest);
            }


            // TODO: scenario 2: intermediate conversion exists between baseCcy/quotedCcy: baseCcy/otherCcy -> otherCcy/quotedCcy

            // scenario 3: no currency pair found to convert
            return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.DuplicateRequest);
        }

        private UserCurrencyConversionResponse CheckUserSettings(long userId, out UserSettings userSettings, Guid requestId, string ccyPair, bool isBuy, decimal amount, int id)
        {
            userSettings = _userSettingsProvider.GetUserSettings(userId);

            //TODO: fill later

            if (userSettings.IsActive == false)
            {
                return GetUserCurrencyConversionResponse( requestId, userId, ccyPair, isBuy, amount, id,  UserConversionEnum.UserInactive);
            }

            if (userSettings.MinTradingAmount > amount )
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedIncorrectMinimumTradingAmount);
            }

            if (userSettings.MaxTradingAmount < amount || userSettings.MaxTradingAmount < userSettings.AvailableBalance)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedIncorrectMaximumTradingAmount);
            }

            if ( amount > userSettings.AvailableBalance)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInsufficientBalance);
            }

            if (!_allCurrencyPairs.Contains(userSettings.UserCcy))
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInvalidCcyPair);
            }

            return null;
        }
        private UserCurrencyConversionResponse CheckIfTheInputDataIsInvalid(Guid requestId, long userId, string ccyPair, 
            bool isBuy, decimal amount, int id)
        {
            // checks
            if (string.IsNullOrEmpty(ccyPair) || !_allCurrencyPairs.Contains(ccyPair))
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInvalidCcyPair);
            }

            if (amount <= 0)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInvalidAmount);

            }

            if (requestId == Guid.Empty)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInvalidRequestId);

            }

            if (userId <= 0)
            {
                return GetUserCurrencyConversionResponse(requestId, userId, ccyPair, isBuy, amount, id, UserConversionEnum.ConversionFailedInvalidUserId);

            }

            return null;
        }
    }
}
