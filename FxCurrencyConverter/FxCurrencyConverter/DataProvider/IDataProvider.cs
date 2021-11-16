using FxCurrencyConverter.CurrencyConverter;
using System.Collections.Generic;

namespace FxCurrencyConverter.DataProvider
{
    public interface IDataProvider
    {
        List<CurrencyPriceDetails> GetCurrencyPriceDetails();
    }
}
