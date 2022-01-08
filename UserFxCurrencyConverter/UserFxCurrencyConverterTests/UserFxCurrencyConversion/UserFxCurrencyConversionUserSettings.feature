Feature: UserFxCurrencyConversionUserSettings
	Simple library for currency conversion based on user settings


Scenario: When a user with Inactive Status places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | IsActive | MinTradingAmount | MaxTradingAmount | AvailableBalance | UserCcy |
	| 1  | 200    | false    | 100              | 10000            | 4000             | GBP     |

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | GBP/USD | Buy  | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | UserInactive     | GBP/USD | 100            | Buy  | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 200    | 87217cb6-bad5-4759-aabd-7e72d9b41c0e | UserInactive     | GBP/USD | 100            | Buy  | null                    |                 |        | null              |



Scenario: When a user with incorrect Min Trading Amount places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | IsActive | MinTradingAmount | MaxTradingAmount | AvailableBalance | UserCcy |
	| 2  | 300    | true     | 100              | 10000            | 4000             | GBP     |
	

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 2  | 300    | 3e571810-9766-406a-88b1-9ce40c5df64d | GBP/USD | Buy  | 10     |
	
	

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult                              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 2  | 300    | 3e571810-9766-406a-88b1-9ce40c5df64d | ConversionFailedIncorrectMinimumTradingAmount | GBP/USD | 10             | Buy  | null                    |                 |        | null              |
	

	And database should store
	| Id | UserId | RequestId                            | ConversionResult                              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 2  | 300    | 3e571810-9766-406a-88b1-9ce40c5df64d | ConversionFailedIncorrectMinimumTradingAmount | GBP/USD | 10             | Buy  | null                    |                 |        | null              |



Scenario: When a user with incorrect Available Balance places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | IsActive | MinTradingAmount | MaxTradingAmount | AvailableBalance | UserCcy |
	| 4  | 500    | true     | 100              | 10000            | 150              | GBP     |
	

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 4  | 500    | 58372ea6-0dc3-4252-8457-ff67f5b43049 | GBP/USD | Buy  | 151    |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult                    | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 4  | 500    | 58372ea6-0dc3-4252-8457-ff67f5b43049 | ConversionFailedInsufficientBalance | GBP/USD | 151            | Buy  | null                    |                 |        | null              |
	

	And database should store
	| Id | UserId | RequestId                            | ConversionResult                    | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 4  | 500    | 58372ea6-0dc3-4252-8457-ff67f5b43049 | ConversionFailedInsufficientBalance | GBP/USD | 151            | Buy  | null                    |                 |        | null              |
	

Scenario: When a user with an invalid Currency places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | IsActive | MinTradingAmount | MaxTradingAmount | AvailableBalance | UserCcy |
	| 5  | 600    | true     | 100              | 10000            | 1550             | ABC     |
	
	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 5  | 600    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | GBP/USD | Sell | 1550   |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 5  | 600    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | ConversionFailedInvalidCcyPair | GBP/USD | 1550           | Sell | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 5  | 600    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | ConversionFailedInvalidCcyPair | GBP/USD | 1550           | Sell | null                    |                 |        | null              |
	

	Scenario: When a user with an no Currency set,  places a request the conversion should not succeed
	Given the database is clean

	And user has below settings:
	| Id | UserId | IsActive | MinTradingAmount | MaxTradingAmount | AvailableBalance | UserCcy |
	| 6  | 700    | true     | 100              | 10000            | 1550             |         |
	
	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 6  | 700    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | GBP/USD | Sell | 1550   |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 5  | 600    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | ConversionFailedInvalidCcyPair | GBP/USD | 1550           | Sell | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 5  | 600    | 58372ea6-0dc3-4252-8457-ff67f5b43050 | ConversionFailedInvalidCcyPair | GBP/USD | 1550           | Sell | null                    |                 |        | null              |


