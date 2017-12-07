Feature: Settings Page
    To have a configurable application 
    I need a settings page where parameters can be stored and updated.
	
@Settings_Page
Scenario: 01 Should Navigate from Main to Settings Page
	Given I am in Main Page
	And There is a 'Settings' button on the page
	When I press the 'Settings' button
	Then the application will Navigate to Settings Page

@Settings_Page
Scenario: 02 Should have a switch to Use Https
    Given I am in Settings page
    And  the settings page has a switch to select Https protocol
    And  I record the state of the UseHttps Setting
    When I tap the switch 
    Then the UseHttps Setting will toggle



