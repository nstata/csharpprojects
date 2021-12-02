namespace UserFxCurrencyConverter.Enums
{
    public enum UserConversionEnum
    {
        Successful = 0,
        DuplicateRequest = 1,
        UserInactive = 2,
        ConversionFailedInvalidCcyPair = 3,
        ConversionFailedIncorrectMinimumTradingAmount = 4,
        ConversionFailedInsufficientBalance = 5,
        ConversionFailedInvalidAmount = 6,
        ConversionFailedInvalidRequestId = 7,
        ConversionFailedInvalidUserId = 8,
        MarketClosed = 9,
        StalePrice = 10,
        Pending = 11,
    }
}
