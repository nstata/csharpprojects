using FxCurrencyConverter.CurrencyConverter;

namespace FxCurrencyConverter.DataProvider
{
    public interface IDataProvider
    {
        CurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair);
    }
}
