Feature: Rest Api
    the app needs to be able to consume RESTful apis 
    in order to send and receive information
    	
@rest_api
Scenario: Should Send data to REST Server
	Given The REST Server is running
    And I have added a '/api/connection' endpoint to return 'connection ok' 
    And I have configured the app for use of REST server endpoint
	And Am in the main page
	When I press Send data button
	Then The server will be called at the endpoint
