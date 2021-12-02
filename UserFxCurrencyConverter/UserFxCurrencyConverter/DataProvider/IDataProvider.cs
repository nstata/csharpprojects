using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.DataProvider
{
    public interface IDataProvider
    {
        UserCurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair);
    }
}
