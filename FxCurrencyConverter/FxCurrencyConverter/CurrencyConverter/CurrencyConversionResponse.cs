using FxCurrencyConverter.Enums;
using System;

namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConversionResponse : CurrencyConversionRequest
    {
        public Guid Id { get; init; }
        public string OriginalAmountCcy { get; init; }

        public string ConvertedAmountCcy { get; init; }

        public decimal? ConvertedAmount { get; init; }

        public decimal? PxUsed { get; init; }

        public ConversionEnum ConversionResults { get; init; }


        public int ConversionResultsId
        {
            get
            {
                return (int)ConversionResults;
            }
        }
    }
}
