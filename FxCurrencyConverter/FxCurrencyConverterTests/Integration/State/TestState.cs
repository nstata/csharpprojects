using FxCurrencyConverter.CurrencyConverter;

namespace FxCurrencyConverterIntegrationTests.State
{
    public class TestState : CurrencyConversionRequest
    {
        public int Id { get; init; }

        public CurrencyConversionResponse ActualResponse { get; set; }
    }
}
