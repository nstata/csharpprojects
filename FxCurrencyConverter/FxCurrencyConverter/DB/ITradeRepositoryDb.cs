using FxCurrencyConverter.CurrencyConverter;

namespace FxCurrencyConverter.DB
{
    public interface ITradeRepositoryDb
    {
        bool InsertIntoFxCurrencyConversionAudit(CurrencyConversionResponse response);
    }
}
