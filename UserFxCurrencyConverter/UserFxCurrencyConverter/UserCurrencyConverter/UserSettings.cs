using UserFxCurrencyConverter.Enums;

namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserSettings
    {
        public long UserId { get; init; }
        public bool IsActive { get; init; }
        public decimal MinTradingAmount { get; init; }
        public decimal MaxTradingAmount { get; init; }
        public decimal AvailableBalance { get; init; }
        public string UserCcy { get; init; }
    }
}
