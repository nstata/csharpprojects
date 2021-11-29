using FxCurrencyConverter.Enums;
using System;


namespace FxCurrencyConverter.CurrencyConverter
{
    public class CurrencyConversionRequest
    {
        public Guid Id { get; init; }

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
