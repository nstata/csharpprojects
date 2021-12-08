Feature: UserFxCurrencyConversionDuplicateRequest
	Simple library for currency conversion based on user settings

Scenario: When previous request was Successful and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 14     | b1457e30-3524-440d-b2f4-0d911403b170 | Successful       | GBP/USD | 200            | Sell |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | GBP/USD | Buy  | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | DuplicateRequest | GBP/USD | 100            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | DuplicateRequest | GBP/USD | 100            | Buy  |                         |                 |        |                   |

	And user settings are not called



Scenario: When previous request was DuplicateRequest and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 14     | b1457e30-3524-440d-b2f4-0d911403b170 | DuplicateRequest | GBP/USD | 200            | Sell |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | GBP/USD | Buy  | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | DuplicateRequest | GBP/USD | 100            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | b1457e30-3524-440d-b2f4-0d911403b170 | DuplicateRequest | GBP/USD | 100            | Buy  |                         |                 |        |                   |

	And user settings are not called


Scenario: When previous request failed due to ConversionFailedInvalidCcyPair and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 11     | d57aadbe-b49f-48e0-b1ba-d54b91edd036 | ConversionFailedInvalidCcyPair | EUR/XXX | 123            | Sell |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 11     | d57aadbe-b49f-48e0-b1ba-d54b91edd036 | EUR/XXX | Sell | 123    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 11     | d57aadbe-b49f-48e0-b1ba-d54b91edd036 | DuplicateRequest | EUR/XXX | 123            | Sell |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 11     | d57aadbe-b49f-48e0-b1ba-d54b91edd036 | DuplicateRequest | EUR/XXX | 123            | Sell |                         |                 |        |                   |

	And user settings are not called


Scenario: When previous request failed due to ConversionFailedInvalidAmount and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 12     | 1aacd4e4-cfef-400e-8e3f-523f2a2fec42 | ConversionFailedInvalidAmount | EUR/CHF | 10             | Sell |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 12     | 1aacd4e4-cfef-400e-8e3f-523f2a2fec42 | EUR/CHF | Sell | 10     |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 12     | 1aacd4e4-cfef-400e-8e3f-523f2a2fec42 | DuplicateRequest | EUR/CHF | 10             | Sell |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 12     | 1aacd4e4-cfef-400e-8e3f-523f2a2fec42 | DuplicateRequest | EUR/CHF | 10             | Sell |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to ConversionFailedInvalidRequestId and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult                 | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 13     | 1756cc35-1b13-4418-ada3-09d2bd7c6549 | ConversionFailedInvalidRequestId | EUR/JPY | 1000           | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 13     | 1756cc35-1b13-4418-ada3-09d2bd7c6549 | EUR/JPY | Buy  | 1000   |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 13     | 1756cc35-1b13-4418-ada3-09d2bd7c6549 | DuplicateRequest | EUR/JPY | 1000           | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 13     | 1756cc35-1b13-4418-ada3-09d2bd7c6549 | DuplicateRequest | EUR/JPY | 1000           | Buy  |                         |                 |        |                   |

	And user settings are not called

	Scenario: When previous request failed due to ConversionFailedInvalidUserId and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 14     | 055be9ed-9683-49f5-a3f6-77e49b3dbe8f | ConversionFailedInvalidUserId | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 14     | 055be9ed-9683-49f5-a3f6-77e49b3dbe8f | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | 055be9ed-9683-49f5-a3f6-77e49b3dbe8f | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 14     | 055be9ed-9683-49f5-a3f6-77e49b3dbe8f | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to UserInactive and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 15     | 9ad8374f-f974-4e1e-90ca-b068aac5a7c0 | UserInactive     | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 15     | 9ad8374f-f974-4e1e-90ca-b068aac5a7c0 | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 15     | 9ad8374f-f974-4e1e-90ca-b068aac5a7c0 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 15     | 9ad8374f-f974-4e1e-90ca-b068aac5a7c0 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to ConversionFailedIncorrectMinimumTradingAmount and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult                              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 16     | 7d22baba-5cd6-4b42-9903-09136b50386b | ConversionFailedIncorrectMinimumTradingAmount | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 16     | 7d22baba-5cd6-4b42-9903-09136b50386b | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 16     | 7d22baba-5cd6-4b42-9903-09136b50386b | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 16     | 7d22baba-5cd6-4b42-9903-09136b50386b | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to ConversionFailedInsufficientBalance and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult                    | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 17     | b909751c-1907-462c-857f-7cf46628ec4e | ConversionFailedInsufficientBalance | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 17     | b909751c-1907-462c-857f-7cf46628ec4e | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 17     | b909751c-1907-462c-857f-7cf46628ec4e | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 17     | b909751c-1907-462c-857f-7cf46628ec4e | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to MarketClosed and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 18     | f40753e1-dc57-4d32-b209-711367ad1163 | MarketClosed     | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 18     | f40753e1-dc57-4d32-b209-711367ad1163 | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 18     | f40753e1-dc57-4d32-b209-711367ad1163 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 18     | f40753e1-dc57-4d32-b209-711367ad1163 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to StalePrice and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 19     | 1495100e-010a-4898-b9c7-3c956c57d080 | StalePrice       | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 19     | 1495100e-010a-4898-b9c7-3c956c57d080 | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 19     | 1495100e-010a-4898-b9c7-3c956c57d080 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 19     | 1495100e-010a-4898-b9c7-3c956c57d080 | DuplicateRequest | EUR/USD | 110            | Buy  |                         |                 |        |                   |

	And user settings are not called


	Scenario: When previous request failed due to UnknownError and the same request is received again it should be marked as Duplicate and the conversion should not succeed
	Given the database is clean

	And we already have below rows in database:
	| UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 20     | faeabb62-81de-4fc2-a63f-881140889297 | UnknownError     | EUR/USD | 110            | Buy  | null                    |                 |        | null              |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 20     | faeabb62-81de-4fc2-a63f-881140889297 | EUR/USD | Buy  | 110    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 20     | faeabb62-81de-4fc2-a63f-881140889297 | DuplicateRequest | EUR/USD | 110            | Buy  | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 20     | faeabb62-81de-4fc2-a63f-881140889297 | DuplicateRequest | EUR/USD | 110            | Buy  | null                    |                 |        | null              |

	And user settings are not called
