     Feature: Login
       In order to access the application
       As a user
       I want to be able to log in

       Scenario: Successful login with valid credentials
         Given I have navigated to the login page
         When I enter valid credentials
         Then I should be logged in successfully