Feature: Note Management
  As a user
  I want to manage my notes
  So that I can keep track of important information

  Scenario: Create a new note
    Given I am on the note creation page
    When I enter the note details
    And I click the save button
    Then the note should be created successfully

  Scenario: Retrieve all notes
    Given I am on the notes page
    When I request to view all notes
    Then I should see a list of all my notes

  Scenario: Update an existing note
    Given I am on the note update page
    When I update the note details
    And I click the update button
    Then the note should be updated successfully

  Scenario: Delete a note
    Given I am on the notes page
    When I delete a note
    Then the note should be deleted successfully
