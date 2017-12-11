Feature: Rest Server Sanity
    In order to test RESTful calls,
    I need a simple server that could act in place of a production one
    	
@rest_sanity
Scenario: Start and Stop Rest server
	Given I have a server set up
    And I have added a '/api/sanity' route to return 'server sane'
	When I call the '/api/sanity' endpoint
	Then the result should be a 'server sane' message

