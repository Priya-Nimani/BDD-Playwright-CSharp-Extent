@E2E
Feature: FirstFeatureFile

Background: 
    Given Playwright home page is loaded

@regression
Scenario Outline:  Test various links on home page    
	Then homepage title is "Playwright111"
	