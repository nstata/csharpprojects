Feature: UserFxCurrencyConversionInvalidSettings
	Simple library for currency conversion based on user settings


Scenario: When a user with invalid RequestID places a request the conversion should not succeed
	Given the database is clean

#	And the database already has below rows:
#	| UserId | RequestId                            | CcyPair | Side | Amount |
#	| 100010 | 00000000-0000-0000-0000-000000000000 | GBP/USD | Buy  | 100    |
#	| 100020 | 00000000-0000-0000-0000-000000000000 | EUR/GBP| Sell | 100    |

	And user has below settings:
	| UserId | TradingStatus | MinTradingAmt | MaxTradingAmt | CurrentBalance | UserCcy | AllowedTradingCcy |
	| 100010 | Active        | 1             | 1000          | 500            | GBP     | GBP/USD,EUR/GBP   |
	| 100020 | Active        | 10000         | 1000000       | 45000          | GBP     | GBP/USD           |


#   allow for test market data

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | GBP/USD | Buy  | 100    |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | GBP/USD | Sell | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult                 | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidUserId    | GBP/USD | 100            | Buy  |                         |                 |        |                   |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Sell |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult                 | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Buy  |                         |                 |        |                   |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Sell |                         |                 |        |                   |

