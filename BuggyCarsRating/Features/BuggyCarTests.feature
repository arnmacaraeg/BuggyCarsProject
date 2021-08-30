Feature: BuggyCarTests

Scenario: Registration - SuccessfulRegister
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| TestPwd1! | TestPwd1!       |
	Then verify the successful registration of new user and is able to login

Scenario: Registration - PasswordDoNotMatch
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| TestPwd1!   | TestPwd2!       |
	Then verify the passwords do not match
	
Scenario: Registration - PasswordMustHaveNumeric
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| TestPwd!   | TestPwd!       |
	Then verify the password have numeric value

Scenario: Registration - PasswordMustHaveSymbol
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| TestPwd1   | TestPwd1       |
	Then verify the password have symbol characters

Scenario: Registration - PasswordMinLength
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| Test       | Test             |
	Then verify the password minimum length

Scenario: Registration - PasswordUpper
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| testpwd1!  | testpwd1!             |
	Then verify the password must have uppercase characters

Scenario: Registration - PasswordLower
	Given that I perform site registration and provide:
	| Password   | Confirm Password |
	| TESTPWD1!  | TESTPWD1!             |
	Then verify the password must have lowercase characters

Scenario Outline: Site Access - Logon
	Given a user account <Username>
	Given a password <Password>
	When the user logs in to the application
	Then the logon result should be <LogonResult>

	Examples:
	| Username      | Password        | LogonResult |
	| angelica_test | TestPwd1!       | Success     |
	| hgsdjgjg      | TestPwd1!       | Fail        |

Scenario: Site Access - Log off
	Given I am logged in
	When I click the log off link
	Then I should be logged off

Scenario: Navigate - Popular Make
    Given I am logged in
	Then I navigate to Popular Make then back to main page successfully

Scenario: Navigate - Popular Model
    Given I am logged in
	Then I navigate to Popular Model then back to main page successfully

Scenario: Navigate - Overall Rating
    Given I am logged in
	Then I navigate to Overall Rating then back to main page successfully

Scenario: Profile Update - Update Profile Details
	Given I am a new user
	When I update my profile details
	Then I verify profile change was saved

Scenario: Voting - Add vote to a model
	Given I am a new user
	When I added a vote to a model
	Then check that my vote was added