Feature: RoomsRealEstate
	In order to review support documents page for DocuSign Rooms
	I need to find this item and verify it

@tag
Scenario: Navigation header on standard screen dimensions
	Given consumer is at the support home page
	When consumer is searching for 'Rooms for Real Estate'
	And search response contains 55 total results
	And consumer is searching and clicking on 'Download and Print Documents - DocuSign Rooms'
	Then thumbs up button from the 'Was this content helpful?' section exists
