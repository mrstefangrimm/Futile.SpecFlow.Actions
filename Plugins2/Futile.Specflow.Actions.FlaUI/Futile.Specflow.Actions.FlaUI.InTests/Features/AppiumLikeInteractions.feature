@Calculator
Feature: Appium-like interactions calls.
The FlaUI plug-in shall have a Appium-like interaction interface for convenience.

Scenario: Add two numbers using Appium-like interactions
	Given Appium-like interactions are set
	And the Calculator.exe with the argument Hello FlaUI from the profile
	And the first number is 12
	And the second number is 13
	When the two numbers are added
	Then the result should be 25
	And "Hello FlaUI" should be displayed
	And number of buttons is 4

Scenario: Subtract two numbers using Appium-like interactions
	Given Appium-like interactions are set
	And the Calculator.exe with the argument Hello FlaUI from the profile
	And the first number is 12
	And the second number is 13
	When the two numbers are subtracted
	Then the result should be -1
	And "Hello FlaUI" should be displayed
