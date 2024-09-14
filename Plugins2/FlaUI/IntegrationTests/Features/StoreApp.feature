@Calculator
Feature: StoreApp
The FlaUI plug-in shall launch windows calculator.

Scenario: Add two numbers
	Given the Windows calculator
	And the number "12" is entered
	And Plus is pressed
	And the number "10" is entered
	When Equal is pressed
	Then the result is 22
