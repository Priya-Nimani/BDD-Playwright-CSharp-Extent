using Microsoft.Playwright;
using PriyaNewProject.PageObjects;
using Reqnroll;
using PriyaNewProject.Support;

namespace PriyaNewProject.StepDefinitions
{
    [Binding]
    public class HomepageStepDef
    {
        private ScenarioContext scenarioContext;
        private readonly HomePage _homePage;

        public HomepageStepDef(Hooks hooks, HomePage homePage, ScenarioContext scenarioContext)
        {
            _homePage = homePage;
            this.scenarioContext = scenarioContext;
        }

        [Given(@"Playwright home page is loaded")]
        public async Task GivenINavigateToTheHomepage()
        {
            await _homePage.GoTo();
        }

        [Then(@"homepage title is {string}")]
        public async Task GivenHomepageTitleIs(string expectedTitle)
        {
            await _homePage.CheckTitleContains(expectedTitle);
        }
    }
}

