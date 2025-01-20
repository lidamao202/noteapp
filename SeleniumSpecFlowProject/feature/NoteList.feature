Feature: NoteList

A short summary of the feature

@tag3
Scenario: Note list page
	Given I am logged in
	Given Set fields on view
		| Title       | Content               |
		| Test Note   | This is a test note   |
		| Test Note 2 | This is a test note 2 |
	When I navigate to the note list page
	Then I should see the note list

