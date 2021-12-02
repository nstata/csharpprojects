using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UserFxCurrencyConverter.UserCurrencyConverter;

namespace UserFxCurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserFxCurrencyConverterController : ControllerBase
    {
        private readonly ILogger<UserFxCurrencyConverterController> _logger;
        private readonly UserCurrencyConverterManager _currencyConverterManager;

        public UserFxCurrencyConverterController(ILogger<UserFxCurrencyConverterController> logger, UserCurrencyConverterManager currencyConverterManager)
        {
            _logger = logger;
            _currencyConverterManager = currencyConverterManager;
        }


        [HttpGet("GetPrice/requestId={requestId},userId={userId},ccyPair={ccyPair},isBuy={isBuy},amount={amount}")]
        public UserCurrencyConversionResponse Get(string ccyPair, bool isBuy, decimal amount, Guid requestId, long userId)
        {
            return _currencyConverterManager.GetCurrencyConversionDetailsForUser(requestId, userId, ccyPair, isBuy, amount);
        }
    }
}
