Feature: AddNoteWithLogin

A short summary of the feature

@tag2

  Scenario: Add a new note
    Given I am logged in
    When I navigate to the add note page
    When I add a new note with title "Test Note" and content "This is a test note"
    Then I should be navigated to the note list page

  Scenario: Add a new note and verify in note list
    Given I am logged in
    When I navigate to the add note page
    And I add a new note with title "Test Note" and content "This is a test note"
    Then I should be navigated to the note list page
    And I should see the note with the title "Test Note" in the notes list
