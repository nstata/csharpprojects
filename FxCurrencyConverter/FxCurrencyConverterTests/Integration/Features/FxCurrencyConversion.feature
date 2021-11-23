Feature: FxCurrencyConversion
	Simple library for currency conversion


Scenario: When using invalid Currency pair conversion should not succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USA      | Buy  | 100    |
	| 2  | GBP/         | Buy  | 100    |
	| 3  | GBP/GBP      | Buy  | 100    |
	| 4  | XXX/YYY      | Sell | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult               | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/USA | 100            |                   | Buy  |
	| 2  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/    | 100            |                   | Buy  |
	| 3  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/GBP | 100            |                   | Buy  |
	| 4  | ConversionFailedInvalidCcyPair |                         |                 |        | XXX/YYY | 100            |                   | Sell |


Scenario: When the amount entered is invalid (zero or negative), conversion should not succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USD      | Buy  | 0      |
	| 2  | GBP/USD      | Buy  | -10    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult              | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | 0              |                   | Buy  |
	| 2  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | -10            |                   | Buy  |
																																			  

Scenario: When the CurrencyPair entered is all lowercase or mix of uppercase and lowercase, conversion should be successful.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | gbp/usd      | Buy  | 100    |
	| 2  | Gbp/usd      | Buy  | 100    |
	| 3  | GBP/Usd      | Buy  | 100    |
	| 4  | gbp/USD      | Buy  | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 2  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 3  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 4  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |




	   
Scenario: When using a valid Currency pair and Market is Closed, conversion should not succeed
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | AUD/CAD      | Buy  | 100    |
	| 2  | AUD/CAD      | Sell | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Buy  |
	| 2  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Sell |


Scenario: When using a valid Currency pair and Market Price is Stale, conversion should not succeed
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | AUD/USD      | Buy  | 100    |
	| 2  | AUD/USD      | Sell | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Buy  |
	| 2  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Sell |
																																  


Scenario: When using a valid Currency pair, market is open and price is latest, conversion should succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USD      | Buy  | 100    |
	| 2  | GBP/USD      | Sell | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 2  | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               | Sell |
