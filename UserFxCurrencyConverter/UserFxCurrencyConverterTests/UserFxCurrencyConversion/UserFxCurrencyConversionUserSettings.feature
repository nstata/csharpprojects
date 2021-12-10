Feature: UserFxCurrencyConversionUserSettings
	Simple library for currency conversion based on user settings


Scenario: When a user with Inactive Status places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | RequestId                            | TradingStatus |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | Inactive      |
	| 2  | 201    | 75100330-532a-46e8-b063-c9810c77cb16 | Inactive      |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | GBP/USD | Buy  | 100    |
	| 2  | 201    | 75100330-532a-46e8-b063-c9810c77cb16 | GBP/USD | Sell | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | UserInactive     | GBP/USD | 100            | Buy  |                         |                 |        |                   |
	| 2  | 201    | 75100330-532a-46e8-b063-c9810c77cb16 | UserInactive     | GBP/USD | 100            | Sell |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | UserInactive     | GBP/USD | 100            | Buy  |                         |                 |        |                   |
	| 2  | 201    | 75100330-532a-46e8-b063-c9810c77cb16 | UserInactive     | GBP/USD | 100            | Sell |                         |                 |        |                   |

	

