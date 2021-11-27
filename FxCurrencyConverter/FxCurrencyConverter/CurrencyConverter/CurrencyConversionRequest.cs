using FxCurrencyConverter.Enums;


namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConversionRequest
    {
        public string CcyPair { get; init; }

        public SideEnum Side { get; init; }

        public decimal OriginalAmount { get; init; }

        public int SideId
        {
            get
            {
                return (int)Side;
            }
        }
    }
}
