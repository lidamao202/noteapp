Feature: AddUpdateNote
  Scenario: Successful login with valid credentials
         Given I have navigated to the login page
         When I enter valid credentials
         Then I should be logged in successfully

Scenario: Add a new note from note list page
    Given I have navigated to the note list page
    When i click on the add note button
    Then I should be navigated to the add note page

  Scenario: Add a new note
    When I add a new note with title "Test Note" and content "This is a test note"
    Then I should be navigated to the note list page

  Scenario: Update an existing note
    Given I have click on edit link to the note list page for "Test Note"
    When I update the note to have title "Updated Note" and content "This is an updated test note"
    Then the note should be updated successfully

  Scenario: View a note
    Given I have navigated to the note list page for "Updated Note"
    Then I should see the note with title "Updated Note" and content "This is an updated test note"