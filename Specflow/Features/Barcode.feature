Feature: Barcode
	I need to have a page that uses the phone camera to read barcodes.
	
@barcode_structure
Scenario: Main page should have a button to navigate to barcode page
    Given I am running the app
    When I examine the main page
    Then It has a button marked as 'Barcode'

@barcode_function
Scenario: Should navigate to barcode page
    Given I am in main page
    When I tap the 'Barcode' button
    Then I go to the barcode page

@barcode_cancel
Scenario: Should return to main page if Cancel is tapped
    Given I am in barcode page
    When  I tap the cancel button
    Then  I go to Main page