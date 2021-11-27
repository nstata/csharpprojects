using FxCurrencyConverter.CurrencyConverter;
using System;

namespace FxCurrencyConverterIntegrationTests.State
{
    public class TestState : CurrencyConversionRequest
    {
        public int Id { get; init; }

        public Guid Guid { get; init;}

        public CurrencyConversionResponse ActualResponse { get; set; }
    }
}
