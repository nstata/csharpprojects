using System.Collections.Generic;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.Interfaces
{
    public interface IDataProvider
    {
        UserCurrencyPriceDetails GetCurrencyPriceDetails(string ccyPair);
        List<string> GetAllCurrencyPairs();
    }
}
