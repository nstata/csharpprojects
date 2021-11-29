Feature: FxCurrencyConversion
	Simple library for currency conversion


Scenario: When using invalid Currency pair conversion should not succeed.
	Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | GBP/USA      | Buy  | 100    | d03f4ed9-179e-4390-a427-c8cb8142403f |
	| 2  | GBP/         | Buy  | 100    | 5bf27ab2-3ff1-4760-af7e-c666aff07a93 |
	| 3  | GBP/GBP      | Buy  | 100    | 0f72cc92-ac01-48ab-ad7f-e25d88f8e4c3 |
	| 4  | XXX/YYY      | Sell | 100    | 28df2e52-7abf-41c5-8d47-e7155135a9b7 |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult               | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/USA | 100            |                   | Buy  | d03f4ed9-179e-4390-a427-c8cb8142403f |
	| 2  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/    | 100            |                   | Buy  | 5bf27ab2-3ff1-4760-af7e-c666aff07a93 |
	| 3  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/GBP | 100            |                   | Buy  | 0f72cc92-ac01-48ab-ad7f-e25d88f8e4c3 |
	| 4  | ConversionFailedInvalidCcyPair |                         |                 |        | XXX/YYY | 100            |                   | Sell | 28df2e52-7abf-41c5-8d47-e7155135a9b7 |

	And database should store
	| Id | ConversionResult               | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/USA | 100            |                   | Buy  | d03f4ed9-179e-4390-a427-c8cb8142403f |
	| 2  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/    | 100            |                   | Buy  | 5bf27ab2-3ff1-4760-af7e-c666aff07a93 |
	| 3  | ConversionFailedInvalidCcyPair |                         |                 |        | GBP/GBP | 100            |                   | Buy  | 0f72cc92-ac01-48ab-ad7f-e25d88f8e4c3 |
	| 4  | ConversionFailedInvalidCcyPair |                         |                 |        | XXX/YYY | 100            |                   | Sell | 28df2e52-7abf-41c5-8d47-e7155135a9b7 |




Scenario: When the amount entered is invalid (zero or negative), conversion should not succeed.
    Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | GBP/USD      | Buy  | 0      | 9bff6400-3f38-4682-acf9-5b0b75669242 |
	| 2  | GBP/USD      | Buy  | -10    | b46e6efb-5930-4b34-86d1-8011d44efded |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult              | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | 0              |                   | Buy  | 9bff6400-3f38-4682-acf9-5b0b75669242 |
	| 2  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | -10            |                   | Buy  | b46e6efb-5930-4b34-86d1-8011d44efded |
	
	And database should store
	| Id | ConversionResult              | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | 0              |                   | Buy  | 9bff6400-3f38-4682-acf9-5b0b75669242 |
	| 2  | ConversionFailedInvalidAmount |                         |                 |        | GBP/USD | -10            |                   | Buy  | b46e6efb-5930-4b34-86d1-8011d44efded |
	
	

Scenario: When the CurrencyPair entered is all lowercase or mix of uppercase and lowercase, conversion should be successful.
	Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | gbp/usd      | Buy  | 100    | 1dc4fa42-2b2f-4a32-9aa4-0920fe257f04 |
	| 2  | Gbp/usd      | Buy  | 100    | fa76ed90-4e88-4c59-88b4-cddc315185f1 |
	| 3  | GBP/Usd      | Buy  | 100    | 216ed28e-3b3f-41e5-a4f1-9e3bba3afa1d |
	| 4  | gbp/USD      | Buy  | 100    | c42bd0bb-ca71-49bb-be0c-a2d316c89399 |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 1dc4fa42-2b2f-4a32-9aa4-0920fe257f04 |
	| 2  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | fa76ed90-4e88-4c59-88b4-cddc315185f1 |
	| 3  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 216ed28e-3b3f-41e5-a4f1-9e3bba3afa1d |
	| 4  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | c42bd0bb-ca71-49bb-be0c-a2d316c89399 |

	And database should store
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 1dc4fa42-2b2f-4a32-9aa4-0920fe257f04 |
	| 2  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | fa76ed90-4e88-4c59-88b4-cddc315185f1 |
	| 3  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 216ed28e-3b3f-41e5-a4f1-9e3bba3afa1d |
	| 4  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | c42bd0bb-ca71-49bb-be0c-a2d316c89399 |




	   
Scenario: When using a valid Currency pair and Market is Closed, conversion should not succeed
	Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | AUD/CAD      | Buy  | 100    | 07e80dc3-3141-4a9b-86da-66bb0f67643c |
	| 2  | AUD/CAD      | Sell | 100    | b2fe2d55-464a-4973-a02f-2cc66c6cbdcf |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Buy  | 07e80dc3-3141-4a9b-86da-66bb0f67643c |
	| 2  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Sell | b2fe2d55-464a-4973-a02f-2cc66c6cbdcf |


	And database should store
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Buy  | 07e80dc3-3141-4a9b-86da-66bb0f67643c |
	| 2  | MarketClosed     |                         |                 |        | AUD/CAD | 100            |                   | Sell | b2fe2d55-464a-4973-a02f-2cc66c6cbdcf |


Scenario: When using a valid Currency pair and Market Price is Stale, conversion should not succeed
	Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | AUD/USD      | Buy  | 100    | 4e10981c-7aa5-4dff-b22c-98609861d2a5 |
	| 2  | AUD/USD      | Sell | 100    | b048af59-3428-4db1-bb00-0a1d3c139406 |
	
	When we run the calculation

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Buy  | 4e10981c-7aa5-4dff-b22c-98609861d2a5 |
	| 2  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Sell | b048af59-3428-4db1-bb00-0a1d3c139406 |
																																  

	And database should store																															 
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Buy  | 4e10981c-7aa5-4dff-b22c-98609861d2a5 |
	| 2  | StalePrice       |                         |                 |        | AUD/USD | 100            |                   | Sell | b048af59-3428-4db1-bb00-0a1d3c139406 |
	

Scenario: When using a valid Currency pair, market is open and price is latest, conversion should succeed.
	Given the database is clean

	And our input is:
	| Id | CurrencyPair | Side | Amount | Guid                                 |
	| 1  | GBP/USD      | Buy  | 100    | 69233754-a113-4355-a1b0-4026bbbf635a |
	| 2  | GBP/USD      | Sell | 100    | 9df429e9-5d73-4330-9f14-72908db4a13a |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 69233754-a113-4355-a1b0-4026bbbf635a |
	| 2  | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               | Sell | 9df429e9-5d73-4330-9f14-72908db4a13a |


	And database should store
	| Id | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side | Guid                                 |
	| 1  | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  | 69233754-a113-4355-a1b0-4026bbbf635a |
	| 2  | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               | Sell | 9df429e9-5d73-4330-9f14-72908db4a13a |
