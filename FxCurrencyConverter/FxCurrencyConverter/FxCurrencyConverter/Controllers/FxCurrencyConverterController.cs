﻿using FxCurrencyConverter.CurrencyConverter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FxCurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[Route("api/[controller]")]
    public class FxCurrencyConverterController : ControllerBase
    {
        private readonly ILogger<FxCurrencyConverterController> _logger;
        private readonly CurrencyConverterManager _currencyConverterManager;

        public FxCurrencyConverterController(ILogger<FxCurrencyConverterController> logger, CurrencyConverterManager currencyConverterManager)
        {
            _logger = logger;
            _currencyConverterManager = currencyConverterManager;
        }


        [HttpGet]
        public IEnumerable<CurrencyPriceDetails> GetCurrencyPrices()
        {
            return _currencyConverterManager.GetCurrencyPrices();
        }


        [HttpGet("GetPrice/ccyPair={ccyPair},isBuy={isBuy},amount={amount}")]
        public CurrencyConversionResponse Get(string ccyPair, bool isBuy, decimal amount)
        {
            // When you buy a currency pair from a forex broker, you buy the base currency and sell the quote currency.
            // Conversely, when you sell the currency pair, you sell the base currency and receive the quote currency.

            // The bid price is the price that the forex broker will buy the base currency from you in exchange for the quote or counter currency.
            // The ask—also called the offer—is the price that the broker will sell you the base currency in exchange for the quote or counter currency.

            return _currencyConverterManager.GetCurrencyConversionDetails(ccyPair, isBuy, amount);
        }
    }
}