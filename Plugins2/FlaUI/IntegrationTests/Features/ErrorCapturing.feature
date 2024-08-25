@ignore @Calculator @Capture
Feature: Error Capturing
The FlaUI plug-in shall support an screenshot or a recording on error.
Use the configuration Screenshot or Recording.

Scenario: Add two numbers
	Given the first number is 12
	And the second number is 13
	When the two numbers are added
	Then the result should be 15
