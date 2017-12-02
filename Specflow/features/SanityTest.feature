Feature: BDD Framework Sanity
    As a developer using BDD techniques
    I want to have a boilerplate testset
    So that I am sure the framework is installed properly
    And I can use it as template for other tests.

@Sanity_BDD
Scenario: Should Framework is installed 
	Given I have installed should framework
    When I set context variable to 30
	Then Should framework confirms that the context variable is equal to 30.
