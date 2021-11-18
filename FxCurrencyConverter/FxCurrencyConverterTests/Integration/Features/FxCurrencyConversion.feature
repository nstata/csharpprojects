Feature: FxCurrencyConversion
	Simple library for currency conversion


Scenario: When using invalid Currency pair conversion should not succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USA      | Buy  | 100    |
	| 2  | GBP/         | Buy  | 100    |
	| 3  | GBP/GBP      | Buy  | 100    |
	| 4  | XXX/YYY      | Buy  | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult               | ConvertedAmountCurrency | ConvertedAmount | PxUsed |
	| 1  | ConversionFailedInvalidCcyPair |                         |                 |        |
	| 2  | ConversionFailedInvalidCcyPair |                         |                 |        |
	| 3  | ConversionFailedInvalidCcyPair |                         |                 |        |
	| 4  | ConversionFailedInvalidCcyPair |                         |                 |        |


Scenario: When the amount entered is invalid (zero or negative), conversion should not succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USD      | Buy  | 0      |
	| 2  | GBP/USD      | Buy  | -10    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult                 | ConvertedAmountCurrency | ConvertedAmount | PxUsed |
	| 1  | ConversionFailedInvalidCcyAmount |                         |                 |        |
	| 2  | ConversionFailedInvalidCcyAmount |                         |                 |        |



Scenario: When the CurrencyPair entered is all lowercase or all uppercase conversion should be successful.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USD      | Buy  | 100    |
	| 2  | gbp/usd      | Buy  | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               |
	| 2  | Successful       | usd                     | 134.272         | 1.34272 | gbp/usd | 100            | GBP               |






	   
Scenario: When using a valid Currency pair, conversion should succeed.
	Given our input is:
	| Id | CurrencyPair | Side | Amount |
	| 1  | GBP/USD      | Buy  | 100    |
	| 2  | GBP/USD      | Sell | 100    |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               |
	| 2  | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               |
