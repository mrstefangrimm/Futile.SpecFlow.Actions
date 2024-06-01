@Calculator
Feature: Executable with Arguments
The FlaUI plug-in shall launch Calculator.exe with arguments defined in the launch profile.

Scenario: Add two numbers with arguments
	Given the Calculator.exe with the argument Hello FlaUI
	And the first number is 12
	And the second number is 13
	When the two numbers are added
	Then the result should be 25
	And "Hello FlaUI" should be displayed
