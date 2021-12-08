using System;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.DB
{
    public interface ITradeRepositoryDb
    {
        int InsertIntoFxCurrencyConversionAudit(Guid requestId, long userId, string ccyPair, bool isBuy, decimal amount);
        void UpdateFxCurrencyConversionAudit(UserCurrencyConversionResponse response);
        bool IsDuplicateRequest(Guid requestId, long userId);
    }
}
