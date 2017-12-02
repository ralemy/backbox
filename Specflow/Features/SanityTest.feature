Feature: Sanity Test
	As a programmer
    who needs to develop app in Xamarin using BDD technique
    I need a simple test to show that the framework is installed and working
	
@Sanity
Scenario: Add two numbers
	Given I store 50 into the feature context under key 'sanityTest'
	When I read the key 'sanityTest' from the context and add 10 to it and store it in 'sanityWhen'
	Then the 'sanityWhen' key in context should be 60.
