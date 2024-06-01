@Calculator
Feature: Executable
The FlaUI plug-in shall launch Calculator.exe which supports add, subtract, multiply and divide of two numbers.

Scenario: Add two numbers
	Given the first number is 12
	And the second number is 13
	When the two numbers are added
	Then the result should be 25
