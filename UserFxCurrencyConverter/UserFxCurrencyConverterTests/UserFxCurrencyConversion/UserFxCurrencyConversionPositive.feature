Feature: UserFxCurrencyConversionValidSettings
	Simple library for currency conversion based on user settings


Scenario: When a user with valid settings places a request she should be allowed to trade
	Given the database is clean

#	And the database already has below rows:
#	| UserId | RequestId                            | CcyPair | Side | Amount |
#	| 100010 | 69233754-a113-4355-a1b0-4026bbbf636a | GBP/USD | Buy  | 100    |
#	| 100020 | 9df429e9-5d73-4330-9f14-72908db4a13a | GBP/USD | Sell | 100    |

	And user has below settings:
	| UserId | TradingStatus | MinTradingAmt | MaxTradingAmt | CurrentBalance | UserCcy | AllowedTradingCcy |
	| 100010 | Active        | 1             | 1000          | 500            | GBP     | GBP/USD,EUR/GBP   |
	| 100020 | Active        | 10000         | 1000000       | 45000          | GBP     | GBP/USD           |

#   allow for test market data

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 100010 | 69233754-a113-4355-a1b0-4026bbbf635a | GBP/USD | Buy  | 100    |
	| 2  | 100020 | 9df429e9-5d73-4330-9f14-72908db4a13a | GBP/USD | Sell | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | 100010 | 69233754-a113-4355-a1b0-4026bbbf635a | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 2  | 100020 | 9df429e9-5d73-4330-9f14-72908db4a13a | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               | Sell |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | ConvertedAmountCurrency | ConvertedAmount | PxUsed  | CcyPair | OriginalAmount | OriginalAmountCcy | Side |
	| 1  | 100010 | 69233754-a113-4355-a1b0-4026bbbf635a | Successful       | USD                     | 134.272         | 1.34272 | GBP/USD | 100            | GBP               | Buy  |
	| 2  | 100020 | 9df429e9-5d73-4330-9f14-72908db4a13a | Successful       | USD                     | 134.126         | 1.34126 | GBP/USD | 100            | GBP               | Sell |

