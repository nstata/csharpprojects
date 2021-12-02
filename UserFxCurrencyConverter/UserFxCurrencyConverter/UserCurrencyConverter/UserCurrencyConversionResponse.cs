using UserFxCurrencyConverter.Enums;

namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserCurrencyConversionResponse : UserCurrencyConversionRequest
    {
        public string OriginalAmountCcy { get; init; }

        public string ConvertedAmountCcy { get; init; }

        public decimal? ConvertedAmount { get; init; }

        public decimal? PxUsed { get; init; }

        public UserConversionEnum ConversionResults { get; init; }


        public int ConversionResultsId
        {
            get
            {
                return (int)ConversionResults;
            }
        }
    }
}
