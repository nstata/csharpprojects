

using FxCurrencyConverter.Enums;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConversionResponse : CurrencyConversionRequest
    {
        public string OriginalAmountCcy { get; init; }

        public string ConvertedAmountCcy { get; init; }

        public decimal? ConvertedAmount { get; init; }

        public decimal? PxUsed { get; init; }

        public ConversionEnum ConversionResults { get; init; }
    }
}
