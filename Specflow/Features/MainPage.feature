Feature: Main Page
    I wish to ensure that the structure and behaviour of the main page is correct
    	
@structure
Scenario: Should be able to start association
	Given I am running the app
	When I examine the main page
	Then It has a button marked as 'Associate'

@function
Scenario: Should navigate to association page
    Given I am in main page
    When I tap the 'Associate' button
    Then I go to the association page
