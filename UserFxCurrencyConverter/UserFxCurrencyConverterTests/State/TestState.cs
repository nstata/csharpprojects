using System;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverterIntegrationTests.State
{
    public class TestState : UserCurrencyConversionRequest
    {
        public int Id { get; init; }

        public Guid RequestId { get; init;}

        public long UserId { get; init; }

        public UserCurrencyConversionResponse ActualResponse { get; set; }
    }
}
