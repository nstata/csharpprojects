Feature: UserFxCurrencyConversionInvalidSettings
	Simple library for currency conversion based on user settings


Scenario: When a user with Invalid RequestId places a request the conversion should not succeed
	Given the database is clean

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | GBP/USD | Buy  | 100    |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | GBP/USD | Sell | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult                 | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Buy  | null                    |                 |        | null              |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Sell | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult                 | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 100010 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Buy  |     null                    |                 |        |  null                 |
	| 2  | 100020 | 00000000-0000-0000-0000-000000000000 | ConversionFailedInvalidRequestId | GBP/USD | 100            | Sell |     null                    |                 |        |  null                 |

	And user settings are not called


Scenario: When a user with Invalid UserId places a request the conversion should not succeed
	Given the database is clean

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 0      | eb63fd00-71c8-41e5-9d67-809d7ae95aa2 | GBP/USD | Buy  | 100    |
	| 2  | -10    | 8efaa640-bd2c-4d6d-8563-47a3784d9e8b | GBP/USD | Sell | 100    |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 0      | eb63fd00-71c8-41e5-9d67-809d7ae95aa2 | ConversionFailedInvalidUserId | GBP/USD | 100            | Buy  | null                    |                 |        | null              |
	| 2  | -10    | 8efaa640-bd2c-4d6d-8563-47a3784d9e8b | ConversionFailedInvalidUserId | GBP/USD | 100            | Sell | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 0      | eb63fd00-71c8-41e5-9d67-809d7ae95aa2 | ConversionFailedInvalidUserId | GBP/USD | 100            | Buy  | null                    |                 |        | null              |
	| 2  | -10    | 8efaa640-bd2c-4d6d-8563-47a3784d9e8b | ConversionFailedInvalidUserId | GBP/USD | 100            | Sell | null                    |                 |        | null              |

	And user settings are not called


Scenario: When the user is using invalid Currency pair conversion should not succeed.
	Given the database is clean
	
	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 1001   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | GBP/USA | Buy  | 100    |
	| 2  | 1002   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d |         | Buy  | 100    |
	| 3  | 1003   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | null    | Buy  | 100    |
	| 4  | 1004   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | GBP/    | Sell | 100    |
	| 5  | 1005   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | /USD    | Buy  | 100    |
	| 6  | 1006   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | GBP/INR | Buy  | 100    |
	
	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 1001   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/USA | 100            | Buy  | null                    |                 |        | null              |
	| 2  | 1002   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair |         | 100            | Buy  | null                    |                 |        | null              |
	| 3  | 1003   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | null    | 100            | Buy  | null                    |                 |        | null              |
	| 4  | 1004   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/    | 100            | Sell | null                    |                 |        | null              |
	| 5  | 1005   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | /USD    | 100            | Buy  | null                    |                 |        | null              |
	| 6  | 1006   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/INR | 100            | Buy  | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult               | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 1001   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/USA | 100            | Buy  | null                    |                 |        | null              |
	| 2  | 1002   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair |         | 100            | Buy  | null                    |                 |        | null              |
	| 3  | 1003   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | null    | 100            | Buy  | null                    |                 |        | null              |
	| 4  | 1004   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/    | 100            | Sell | null                    |                 |        | null              |
	| 5  | 1005   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | /USD    | 100            | Buy  | null                    |                 |        | null              |
	| 6  | 1006   | 96a9cc7c-d978-4b07-ae0d-958e406adb5d | ConversionFailedInvalidCcyPair | GBP/INR | 100            | Buy  | null                    |                 |        | null              |

	And user settings are not called


Scenario: When a user enters invalid (zero or negative) amount, conversion should not succeed.
    Given the database is clean

	And the request received is:
	| Id | UserId | RequestId                            | CcyPair | Side | Amount |
	| 1  | 101    | cae799bd-4f58-4ab8-a2bd-686e424ef8ba | EUR/GBP | Buy  | -100   |
	| 2  | 102    | 3b32c1a2-d066-4d1f-a687-22deab727828 | GBP/USD | Sell | 0      |

	When we run the calculation with latest market price

	Then the expected results should be
	| Id | UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 101    | cae799bd-4f58-4ab8-a2bd-686e424ef8ba | ConversionFailedInvalidAmount | EUR/GBP | -100           | Buy  | null                    |                 |        | null              |
	| 2  | 102    | 3b32c1a2-d066-4d1f-a687-22deab727828 | ConversionFailedInvalidAmount | GBP/USD | 0              | Sell | null                    |                 |        | null              |

	And database should store
	| Id | UserId | RequestId                            | ConversionResult              | CcyPair | OriginalAmount | Side | ConvertedAmountCurrency | ConvertedAmount | PxUsed | OriginalAmountCcy |
	| 1  | 101    | cae799bd-4f58-4ab8-a2bd-686e424ef8ba | ConversionFailedInvalidAmount | EUR/GBP | -100           | Buy  | null                    |                 |        | null              |
	| 2  | 102    | 3b32c1a2-d066-4d1f-a687-22deab727828 | ConversionFailedInvalidAmount | GBP/USD | 0              | Sell | null                    |                 |        | null              |

	And user settings are not called
