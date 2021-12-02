using UserFxCurrencyConverter.Enums;

namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserCurrencyPriceDetails
    {
        public string CcyPair { get; init; }
        public decimal BidPx { get; init; }
        public decimal AskPx { get; init; }
        public UserMarketPriceStateEnum PriceState { get; init; }
        public System.DateTime LastUpdated { get; init; }
    }
}
