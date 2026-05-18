Feature: This is third scenario to test

@smoke
Scenario: Test Get Started page    
    Given Playwright home page is loaded
	Then homepage title is "Playwright"
    When I click on Get Started link
    Then the page has a heading with the name of "Installation"