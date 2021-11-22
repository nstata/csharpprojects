namespace FxCurrencyConverter.Enums
{
    public enum ConversionEnum
    {
        Successful = 0,
        ConversionFailedInvalidCcyPair = 1,
        ConversionFailedInvalidAmount = 2,
        MarketClosed = 3,
        StalePrice = 4,
        //RatesCrossed = 2,
        //InsufficientAmount = 3,
        //BothAmountsSpecified = 4,
    }
}
