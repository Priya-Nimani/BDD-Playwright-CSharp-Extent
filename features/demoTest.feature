@E2E
Feature: FirstFeatureFile

@regression
Scenario:  Test various links on home page   
    Given Playwright home page is loaded
	Then homepage title is "Playwright111"
	