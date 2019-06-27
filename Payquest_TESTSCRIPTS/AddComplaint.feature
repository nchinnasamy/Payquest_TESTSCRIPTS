Feature: AddComplaint
	To create a complaint for a authorized user

@mytag
Scenario: Create Complaint from the file action
	Given I have logged into the application
	And I have navigated to the complaint interface
	When I raise complaint
	| keyword        |  | value |
	| Complaint Type |  |   Internal-minor    |
	Then the debt status is complaint
