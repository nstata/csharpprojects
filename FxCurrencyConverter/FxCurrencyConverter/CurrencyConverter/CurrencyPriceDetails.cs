using FxCurrencyConverter.Enums;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyPriceDetails
    {
        public string CcyPair { get; init; }
        public decimal BidPx { get; init; }
        public decimal AskPx { get; init; }

        public MarketPriceStateEnum PriceState { get; init; }
        public System.DateTime LastUpdated { get; init; }
    }
}
