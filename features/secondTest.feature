Feature: This is second feature file 

Background: 
    Given Playwright home page is loaded

@E2E
Scenario:  Test various links on home page    
	Then homepage title is "Playwright"
	