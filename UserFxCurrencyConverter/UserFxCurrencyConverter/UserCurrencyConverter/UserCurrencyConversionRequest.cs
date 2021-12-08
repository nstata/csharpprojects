using System;
using UserFxCurrencyConverter.Enums;


namespace UserFxCurrencyConverter.UserCurrencyConverter
{
    public class UserCurrencyConversionRequest
    {
        public long ID { get; init; }

        public Guid RequestId { get; init; }

        public long UserId { get; init; }

        public string CcyPair { get; init; }

        public UserSideEnum Side { get; init; }

        public decimal OriginalAmount { get; init; }

        public UserSideEnum RequestStatus { get; init; }


        public int SideId
        {
            get
            {
                return (int)Side;
            }
        }
    }

}
